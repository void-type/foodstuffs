﻿using FoodStuffs.Model.Data;
using FoodStuffs.Model.Data.Models;
using FoodStuffs.Model.Data.Queries;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VoidCore.EntityFramework;
using VoidCore.Model.Functional;
using VoidCore.Model.Responses.Collections;
using C = FoodStuffs.Model.Search.RecipeSearchConstants;

namespace FoodStuffs.Model.Search;

public class RecipeIndexService : IRecipeIndexService
{
    private const int BATCH_SIZE = 100;

    private readonly ILogger<RecipeIndexService> _logger;
    private readonly RecipeSearchSettings _settings;
    private readonly FoodStuffsContext _data;

    public RecipeIndexService(ILogger<RecipeIndexService> logger, RecipeSearchSettings settings, FoodStuffsContext data)
    {
        _logger = logger;
        _settings = settings;
        _data = data;
    }

    public async Task AddOrUpdate(int recipeId, CancellationToken cancellationToken)
    {
        var byId = new RecipesWithAllRelatedSpecification(recipeId);

        var maybeRecipe = await _data.Recipes
            .TagWith($"{nameof(RecipeIndexService)}.{nameof(AddOrUpdate)}({nameof(RecipesWithAllRelatedSpecification)})")
            .AsSplitQuery()
            .ApplyEfSpecification(byId)
            .OrderBy(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken)
            .MapAsync(Maybe.From);

        if (maybeRecipe.HasNoValue)
        {
            return;
        }

        var recipe = maybeRecipe.Value;
        AddOrUpdate(recipe);
    }

    public void AddOrUpdate(Recipe recipe)
    {
        using var writers = new LuceneWriters(_settings, C.LUCENE_VERSION, OpenMode.CREATE_OR_APPEND);
        // Ensure index
        writers.IndexWriter.Commit();
        writers.TaxonomyWriter.Commit();

        var facetsConfig = RecipeSearchMappers.RecipeFacetsConfig();

        var doc = facetsConfig.Build(writers.TaxonomyWriter, recipe.ToDocument());

        if (ExistsInIndex(recipe.Id))
        {
            writers.IndexWriter.UpdateDocument(new Term(C.FIELD_ID, recipe.Id.ToString()), doc);
        }
        else
        {
            writers.IndexWriter.AddDocument(doc);
        }

        writers.IndexWriter.Commit();
        writers.TaxonomyWriter.Commit();
    }

    public async Task Rebuild(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting rebuild of recipe search index.");

        using var writers = new LuceneWriters(_settings, C.LUCENE_VERSION, OpenMode.CREATE);

        var facetsConfig = RecipeSearchMappers.RecipeFacetsConfig();

        var page = 1;
        var numIndexed = 0;
        var done = false;

        do
        {
            var pagination = new PaginationOptions(page, BATCH_SIZE);
            var withAllRelated = new RecipesWithAllRelatedSpecification();

            var recipes = await _data.Recipes
                .TagWith($"{nameof(RecipeIndexService)}.{nameof(Rebuild)}({nameof(RecipesWithAllRelatedSpecification)})")
                .AsSplitQuery()
                .ApplyEfSpecification(withAllRelated)
                .OrderBy(x => x.Id)
                .GetPage(pagination)
                .ToListAsync(cancellationToken);

            foreach (var recipe in recipes)
            {
                var builtDoc = facetsConfig.Build(writers.TaxonomyWriter, recipe.ToDocument());
                writers.IndexWriter.AddDocument(builtDoc);
                numIndexed++;
            }

            done = recipes.Count < 1;
            page++;
        } while (!done);

        writers.IndexWriter.Commit();
        writers.TaxonomyWriter.Commit();

        _logger.LogInformation("Finished rebuild of recipe search index. {DocCount} documents.", numIndexed);
    }

    public void Remove(int recipeId)
    {
        using var writers = new LuceneWriters(_settings, C.LUCENE_VERSION, OpenMode.CREATE_OR_APPEND);
        // Ensure index
        writers.IndexWriter.Commit();
        writers.TaxonomyWriter.Commit();

        writers.IndexWriter.DeleteDocuments(new Term(C.FIELD_ID, recipeId.ToString()));

        writers.IndexWriter.Commit();
    }

    private bool ExistsInIndex(int recipeId)
    {
        using var readers = new LuceneReaders(_settings);

        var query = new TermQuery(new Term(C.FIELD_ID, recipeId.ToString()));
        var topDocs = readers.IndexSearcher.Search(query, 1);

        return topDocs.TotalHits > 0;
    }
}

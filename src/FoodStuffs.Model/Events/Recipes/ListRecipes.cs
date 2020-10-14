﻿using FoodStuffs.Model.Data;
using FoodStuffs.Model.Data.Models;
using FoodStuffs.Model.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using VoidCore.Domain;
using VoidCore.Domain.Events;
using VoidCore.Model.Logging;
using VoidCore.Model.Responses.Collections;

// Allow single file events
#pragma warning disable CA1034

namespace FoodStuffs.Model.Events.Recipes
{
    public static class ListRecipes
    {
        public class Handler : EventHandlerAbstract<Request, IItemSet<RecipeListItemDto>>
        {
            private readonly IFoodStuffsData _data;

            public Handler(IFoodStuffsData data)
            {
                _data = data;
            }

            public override async Task<IResult<IItemSet<RecipeListItemDto>>> Handle(Request request, CancellationToken cancellationToken = default)
            {
                var paginationOptions = new PaginationOptions(request.Page, request.Take, request.IsPagingEnabled);

                var searchCriteria = GetSearchCriteria(request);

                var allSearch = new RecipesSearchSpecification(searchCriteria);

                var totalCount = await _data.Recipes.Count(allSearch, cancellationToken).ConfigureAwait(false);

                var pagedSearch = new RecipesSearchSpecification(
                    criteria: searchCriteria,
                    paginationOptions: paginationOptions,
                    sortBy: request.SortBy,
                    sortDesc: request.SortDesc);

                var recipes = await _data.Recipes.List(pagedSearch, cancellationToken).ConfigureAwait(false);

                return recipes
                    .Select(r => new RecipeListItemDto(
                        id: r.Id,
                        name: r.Name,
                        categories: r.CategoryRecipe.Select(cr => cr.Category.Name).OrderBy(n => n),
                        imageId: r.PinnedImageId ?? (r.Image.Count > 0 ? r.Image.Select(i => i.Id).FirstOrDefault() : (int?)null)))
                    .ToItemSet(paginationOptions, totalCount)
                    .Map(Ok);
            }

            private static Expression<Func<Recipe, bool>>[] GetSearchCriteria(Request request)
            {
                var searchCriteria = new List<Expression<Func<Recipe, bool>>>();

                if (!string.IsNullOrWhiteSpace(request.NameSearch))
                {
                    searchCriteria.Add(recipe => recipe.Name.ToLower().Contains(request.NameSearch.ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(request.CategorySearch))
                {
                    searchCriteria.Add(recipe => recipe.CategoryRecipe.Any(cr => cr.Category.Name.ToLower().Contains(request.CategorySearch.ToLower())));
                }

                return searchCriteria.ToArray();
            }
        }

        public class Request
        {
            public Request(string? nameSearch, string? categorySearch, string? sortBy, bool sortDesc, bool isPagingEnabled, int page, int take)
            {
                NameSearch = nameSearch;
                CategorySearch = categorySearch;
                SortBy = sortBy;
                SortDesc = sortDesc;
                IsPagingEnabled = isPagingEnabled;
                Page = page;
                Take = take;
            }

            public string? NameSearch { get; }
            public string? CategorySearch { get; }
            public string? SortBy { get; }
            public bool SortDesc { get; }
            public bool IsPagingEnabled { get; }
            public int Page { get; }
            public int Take { get; }
        }

        public class RecipeListItemDto
        {
            public RecipeListItemDto(int id, string name, IEnumerable<string> categories, int? imageId)
            {
                Id = id;
                Name = name;
                Categories = categories;
                ImageId = imageId;
            }

            public int Id { get; }
            public string Name { get; }
            public IEnumerable<string> Categories { get; }
            public int? ImageId { get; }
        }

        public class Logger : ItemSetEventLogger<Request, RecipeListItemDto>
        {
            public Logger(ILoggingService logger) : base(logger) { }

            protected override void OnBoth(Request request, IResult<IItemSet<RecipeListItemDto>> result)
            {
                Logger.Info(
                    $"RequestNameSearch: {request.NameSearch}",
                    $"RequestCategorySearch: {request.CategorySearch}",
                    $"RequestSort: {request.SortBy}",
                    $"RequestIsPagingEnabled: {request.IsPagingEnabled}",
                    $"RequestPage: {request.Page}",
                    $"RequestTake: {request.Take}"
                );

                base.OnBoth(request, result);
            }
        }
    }
}

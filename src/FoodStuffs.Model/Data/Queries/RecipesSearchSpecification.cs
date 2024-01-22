﻿using FoodStuffs.Model.Data.Models;
using System.Linq.Expressions;
using VoidCore.Model.Data;
using VoidCore.Model.Responses.Collections;

namespace FoodStuffs.Model.Data.Queries;

public class RecipesSearchSpecification : QuerySpecificationAbstract<Recipe>
{
    public RecipesSearchSpecification(Expression<Func<Recipe, bool>>[] criteria, PaginationOptions paginationOptions, string? sortBy = null) : base(criteria)
    {
        ApplyPaging(paginationOptions);

        AddInclude(nameof(Recipe.Categories));
        AddInclude(nameof(Recipe.Images));
        AddInclude(nameof(Recipe.Ingredients));

        // We handle random in the handler because EF can't order by unstable sorts.
        switch (sortBy?.ToUpperInvariant())
        {
            case "NEWEST":
                AddOrderBy(recipe => recipe.CreatedOn, true);
                break;

            case "OLDEST":
                AddOrderBy(recipe => recipe.CreatedOn);
                break;

            case "A-Z":
                AddOrderBy(recipe => recipe.Name);
                AddOrderBy(recipe => recipe.Id);
                break;

            case "Z-A":
                AddOrderBy(recipe => recipe.Name, true);
                AddOrderBy(recipe => recipe.Id, true);
                break;

            default:
                AddOrderBy(recipe => recipe.Id, true);
                break;
        }
    }
}

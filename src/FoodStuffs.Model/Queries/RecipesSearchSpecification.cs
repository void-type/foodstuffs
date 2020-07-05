﻿using FoodStuffs.Model.Data.Models;
using System;
using System.Linq.Expressions;
using VoidCore.Model.Data;
using VoidCore.Model.Responses.Collections;

namespace FoodStuffs.Model.Queries
{
    public class RecipesSearchSpecification : QuerySpecificationAbstract<Recipe>
    {
        public RecipesSearchSpecification(Expression<Func<Recipe, bool>>[] criteria) : base(criteria)
        {
            AddInclude($"{nameof(Recipe.CategoryRecipe)}.{nameof(CategoryRecipe.Category)}");
        }

        public RecipesSearchSpecification(Expression<Func<Recipe, bool>>[] criteria, PaginationOptions paginationOptions, string sortBy = null, bool sortDesc = false) : this(criteria)
        {
            ApplyPaging(paginationOptions);

            switch (sortBy)
            {
                case "name":
                    ApplyOrderByWithDescendingFlag(recipe => recipe.Name, sortDesc);
                    AddThenBy(recipe => recipe.Id);
                    break;

                default:
                    ApplyOrderByDescending(recipe => recipe.Id);
                    break;
            }
        }

        private void ApplyOrderByWithDescendingFlag(Expression<Func<Recipe, object>> sortPropertySelector, bool isDesc)
        {
            if (isDesc)
            {
                ApplyOrderByDescending(sortPropertySelector);
            }
            else
            {
                ApplyOrderBy(sortPropertySelector);
            }
        }
    }
}

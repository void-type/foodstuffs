﻿using Core.Model.Validation;
using FoodStuffs.Model.ViewModels;
using System.Linq;

namespace FoodStuffs.Model.Validation
{
    public class RecipeViewModelValidator : AbstractSimpleValidator<IRecipeViewModel>
    {
        protected override void RunRules(IRecipeViewModel entity)
        {
            Invalid("name", "Please enter a name.")
                .When(() => string.IsNullOrWhiteSpace(entity.Name));

            Invalid("ingredients", "Please enter ingredients.")
                .When(() => string.IsNullOrWhiteSpace(entity.Ingredients));

            Invalid("directions", "Please enter directions.")
                .When(() => string.IsNullOrWhiteSpace(entity.Directions));

            Invalid("cookTimeMinutes", "Cook time must be positive.")
                .When(() => entity.CookTimeMinutes < 0)
                .Suppress(() => entity.CookTimeMinutes == null);

            Invalid("prepTimeMinutes", "Prep time must be positive.")
                .When(() => entity.PrepTimeMinutes < 0)
                .Suppress(() => entity.PrepTimeMinutes == null);

            Invalid("categories", "One or more categories is invalid.")
                .When(() => entity.Categories.Any(string.IsNullOrWhiteSpace));
        }
    }
}
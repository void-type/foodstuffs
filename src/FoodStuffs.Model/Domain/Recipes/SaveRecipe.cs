using FoodStuffs.Model.Data;
using FoodStuffs.Model.Data.Models;
using FoodStuffs.Model.Queries;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using VoidCore.Model.ClientApp;
using VoidCore.Model.Data;
using VoidCore.Model.Domain;
using VoidCore.Model.Logging;
using VoidCore.Model.Responses.Messages;
using VoidCore.Model.Time;
using VoidCore.Model.Validation;

namespace FoodStuffs.Model.Domain.Recipes
{
    public class SaveRecipe
    {
        public class Handler : EventHandlerSyncAbstract<Request, PostSuccessUserMessage<int>>
        {
            public Handler(IFoodStuffsData data, IDateTimeService now, ICurrentUserAccessor userAccessor, IAuditUpdater auditUpdater)
            {
                _data = data;
                _now = now.Moment;
                _user = userAccessor;
                _auditUpdater = auditUpdater;
            }

            protected override Result<PostSuccessUserMessage<int>> HandleSync(Request request)
            {
                var maybeRecipe = Maybe.From(_data.Recipes.Stored
                    .WhereById(request.Id)
                    .FirstOrDefault());

                var recipe = maybeRecipe.HasValue ? maybeRecipe.Value : CreateRecipe();

                recipe.Name = request.Name;
                recipe.Ingredients = request.Ingredients;
                recipe.Directions = request.Directions;
                recipe.CookTimeMinutes = request.CookTimeMinutes;
                recipe.PrepTimeMinutes = request.PrepTimeMinutes;
                _auditUpdater.Update(recipe);

                var formattedRequestCategories = FormatCategoryNames(request.Categories);

                var currentCategoriesAndCategoryRecipes = _data.CategoryRecipes.Stored
                    .WhereForRecipe(recipe.Id)
                    .Join(_data.Categories.Stored,
                        cr => cr.CategoryId,
                        c => c.Id,
                        (cr, c) => new { Category = c, CategoryRecipe = cr });

                var categoriesToCreate = formattedRequestCategories
                    .Where(name => !_data.Categories.Stored
                        .Select(c => c.Name)
                        .Contains(name))
                    .Select(CreateCategory);

                _data.Categories.AddRange(categoriesToCreate);

                var categoryRecipesToRemove = currentCategoriesAndCategoryRecipes
                    .Where(crc => !formattedRequestCategories.Contains(crc.Category.Name))
                    .Select(crc => crc.CategoryRecipe);

                _data.CategoryRecipes.RemoveRange(categoryRecipesToRemove);

                var categoryRecipesToCreate = _data.Categories.Stored
                    .Where(c => formattedRequestCategories.Contains(c.Name))
                    .Where(c => !currentCategoriesAndCategoryRecipes
                        .Select(crc => crc.Category.Name)
                        .Contains(c.Name))
                    .Select(c => CreateCategoryRecipe(recipe.Id, c));

                _data.CategoryRecipes.AddRange(categoryRecipesToCreate);

                _data.SaveChanges();

                return Result.Ok(PostSuccessUserMessage.Create("Recipe saved.", request.Id));
            }

            private CategoryRecipe CreateCategoryRecipe(int recipeId, Category category)
            {
                var categoryRecipe = _data.CategoryRecipes.New;
                categoryRecipe.RecipeId = recipeId;
                categoryRecipe.CategoryId = category.Id;
                return categoryRecipe;
            }

            private Category CreateCategory(string viewModelCategory)
            {
                var category = _data.Categories.New;
                category.Name = viewModelCategory;
                return category;
            }

            private string[] FormatCategoryNames(IEnumerable<string> categories)
            {
                return categories
                    .Where(category => !string.IsNullOrWhiteSpace(category))
                    .Select(category => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(category.Trim()))
                    .ToArray();
            }

            private Recipe CreateRecipe()
            {
                var recipe = _data.Recipes.New;
                _auditUpdater.Create(recipe);
                return recipe;
            }

            private readonly IFoodStuffsData _data;
            private readonly DateTime _now;
            private readonly ICurrentUserAccessor _user;
            private readonly IAuditUpdater _auditUpdater;
        }

        public class Request
        {
            public Request(int id, string name, string ingredients, string directions, int? cookTimeMinutes, int? prepTimeMinutes, IEnumerable<string> categories)
            {
                Id = id;
                Name = name;
                Ingredients = ingredients;
                Directions = directions;
                CookTimeMinutes = cookTimeMinutes;
                PrepTimeMinutes = prepTimeMinutes;
                Categories = categories;
            }

            public int Id { get; }
            public string Name { get; }
            public string Ingredients { get; }
            public string Directions { get; }
            public int? CookTimeMinutes { get; }
            public int? PrepTimeMinutes { get; }
            public IEnumerable<string> Categories { get; }
        }

        public class RequestValidator : RuleValidatorAbstract<Request>
        {
            protected override void BuildRules()
            {
                CreateRule("Please enter a name.", "name")
                    .InvalidWhen(entity => string.IsNullOrWhiteSpace(entity.Name));

                CreateRule("Please enter ingredients.", "ingredients")
                    .InvalidWhen(entity => string.IsNullOrWhiteSpace(entity.Ingredients));

                CreateRule("Please enter directions.", "directions")
                    .InvalidWhen(entity => string.IsNullOrWhiteSpace(entity.Directions));

                CreateRule("Cook time must be positive.", "cookTimeMinutes")
                    .InvalidWhen(entity => entity.CookTimeMinutes < 0)
                    .ExceptWhen(entity => entity.CookTimeMinutes == null);

                CreateRule("Prep time must be positive.", "prepTimeMinutes")
                    .InvalidWhen(entity => entity.PrepTimeMinutes < 0)
                    .ExceptWhen(entity => entity.PrepTimeMinutes == null);
            }
        }

        public class Logger : PostSuccessUserMessageEventLogger<Request, int>
        {
            public Logger(ILoggingService logger) : base(logger) { }
        }
    }
}

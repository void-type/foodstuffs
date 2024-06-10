﻿namespace FoodStuffs.Model.Events.MealPlans;

public record GetMealPlanResponse(
    int Id,
    string Name,
    string CreatedBy,
    DateTimeOffset CreatedOn,
    string ModifiedBy,
    DateTimeOffset ModifiedOn,
    List<GetMealPlanResponsePantryShoppingItem> PantryShoppingItems,
    List<GetMealPlanResponseRecipe> Recipes);

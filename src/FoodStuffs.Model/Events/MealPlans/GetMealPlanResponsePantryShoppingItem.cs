﻿namespace FoodStuffs.Model.Events.MealPlans;

public record GetMealPlanResponsePantryShoppingItem(
    int Id,
    string Name,
    decimal Quantity);

﻿namespace FoodStuffs.Model.Events.MealPlans;

public record SaveMealPlanRequestPantryIngredient(
    string Name,
    decimal Quantity);
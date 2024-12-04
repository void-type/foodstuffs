﻿namespace FoodStuffs.Model.Events.ShoppingItems;

public record GetShoppingItemResponse(
    int Id,
    string Name,
    string CreatedBy,
    DateTimeOffset CreatedOn,
    string ModifiedBy,
    DateTimeOffset ModifiedOn,
    List<GetShoppingItemResponseRecipe> Recipes);

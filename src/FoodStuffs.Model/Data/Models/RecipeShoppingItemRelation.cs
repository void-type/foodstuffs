﻿namespace FoodStuffs.Model.Data.Models;

public class RecipeShoppingItemRelation
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public int Order { get; set; }

    public ShoppingItem ShoppingItem { get; set; } = null!;
}

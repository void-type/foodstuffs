﻿namespace FoodStuffs.Model.Data.Models
{
    public partial class CategoryRecipe
    {
        public int CategoryId { get; set; }
        public int RecipeId { get; set; }

        public Category Category { get; set; }
        public Recipe Recipe { get; set; }
    }
}

﻿using FoodStuffs.Model.Interfaces.Domain;
using System.Collections.Generic;
using System.Linq;

namespace FoodStuffs.Data.FoodStuffsDb.Models
{
    public partial class Category : ICategory
    {
        ICollection<ICategoryRecipe> ICategory.CategoryRecipe
        {
            get => CategoryRecipe.Select(x => (ICategoryRecipe)x).ToArray();
            set => CategoryRecipe = value.Select(x => (CategoryRecipe)x).ToArray();
        }
    }
}
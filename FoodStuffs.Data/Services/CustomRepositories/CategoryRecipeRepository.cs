﻿using Core.Data.EntityFramework;
using FoodStuffs.Data.Models;
using FoodStuffs.Model.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FoodStuffs.Data.Services.CustomRepositories
{
    public class CategoryRecipeRepository : EfWritableRepository<ICategoryRecipe, CategoryRecipe>
    {
        public override IQueryable<ICategoryRecipe> Stored => Context.Set<CategoryRecipe>()
            .Include(cr => cr.Category)
            .Include(cr => cr.Recipe);

        public CategoryRecipeRepository(DbContext context) : base(context)
        {
        }
    }
}
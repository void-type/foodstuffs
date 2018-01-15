﻿using FoodStuffs.Data.Models;
using FoodStuffs.Model.Interfaces.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Core.Data.EntityFramework;

namespace FoodStuffs.Data.CustomRepositories
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
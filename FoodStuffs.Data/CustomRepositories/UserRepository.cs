﻿using FoodStuffs.Data.Models;
using FoodStuffs.Model.Interfaces.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Core.Data.EntityFramework;

namespace FoodStuffs.Data.CustomRepositories
{
    public class UserRepository : EfWritableRepository<IUser, User>
    {
        public override IQueryable<IUser> Stored => Context.Set<User>()
            .Include(u => u.RecipeModifiedByUser)
            .Include(u => u.RecipeCreatedByUser);

        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}
﻿using FoodStuffs.Model.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using VoidCore.AspNet.Data;

namespace FoodStuffs.Web.Data
{
    public class UserRepository : EfWritableRepository<User>
    {
        public override IQueryable<User> Stored => Context.Set<User>();

        public UserRepository(DbContext context) : base(context) { }
    }
}

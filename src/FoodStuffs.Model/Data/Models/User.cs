﻿using System.Collections.Generic;

namespace FoodStuffs.Model.Data.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}

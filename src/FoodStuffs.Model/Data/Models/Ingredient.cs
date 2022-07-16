﻿namespace FoodStuffs.Model.Data.Models;

public partial class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }
    public int Order { get; set; }
    public bool IsCategory { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime CreatedOn { get; set; }
    public string ModifiedBy { get; set; } = null!;
    public DateTime ModifiedOn { get; set; }
    public int RecipeId { get; set; }

    public virtual Recipe Recipe { get; set; } = null!;
}

﻿using FoodStuffs.Model.Events.ShoppingItems.Models;
using VoidCore.Model.Functional;
using VoidCore.Model.RuleValidator;

namespace FoodStuffs.Model.Events.ShoppingItems;

public class SaveShoppingItemRequestValidator : RuleValidatorAbstract<SaveShoppingItemRequest>
{
    public SaveShoppingItemRequestValidator()
    {
        CreateRule(new Failure("Shopping item must have a name.", "name"))
            .InvalidWhen(entity => string.IsNullOrWhiteSpace(entity.Name));

        CreateRule(new Failure("Shopping item name can't be longer than 450 characters.", "name"))
            .InvalidWhen(entity => entity.Name.Length > 450);
    }
}

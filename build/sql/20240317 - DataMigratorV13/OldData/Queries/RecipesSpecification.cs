﻿using DataMigratorV13.OldData.Models;
using VoidCore.Model.Data;

namespace DataMigratorV13.OldData.Queries;

public class RecipesSpecification : QuerySpecificationAbstract<Recipe>
{
    public RecipesSpecification(int id)
    {
        AddCriteria(r => r.Id == id);
    }

    public RecipesSpecification(IEnumerable<int> ids)
    {
        AddCriteria(r => ids.Contains(r.Id));
    }
}

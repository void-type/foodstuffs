﻿using FoodStuffs.Model.Data;
using FoodStuffs.Model.Data.Models;
using FoodStuffs.Model.Data.Queries;
using System.Linq.Expressions;
using VoidCore.Model.Events;
using VoidCore.Model.Functional;
using VoidCore.Model.Responses.Collections;

namespace FoodStuffs.Model.Events.MealSets;

public class ListMealSetsHandler : EventHandlerAbstract<ListMealSetsRequest, IItemSet<ListMealSetsResponse>>
{
    private readonly IFoodStuffsData _data;

    public ListMealSetsHandler(IFoodStuffsData data)
    {
        _data = data;
    }

    public override Task<IResult<IItemSet<ListMealSetsResponse>>> Handle(ListMealSetsRequest request, CancellationToken cancellationToken = default)
    {
        var paginationOptions = request.GetPaginationOptions();

        var searchCriteria = GetSearchCriteria(request);

        var pagedSearch = new MealSetsSpecification(
            criteria: searchCriteria,
            paginationOptions: paginationOptions);

        return _data.MealSets.ListPage(pagedSearch, cancellationToken)
            .SelectAsync(ms => new ListMealSetsResponse(
                Id: ms.Id,
                Name: ms.Name))
            .MapAsync(Ok);
    }

    private static Expression<Func<MealSet, bool>>[] GetSearchCriteria(ListMealSetsRequest request)
    {
        var searchCriteria = new List<Expression<Func<MealSet, bool>>>();

        // StringComparison overloads aren't supported in EF's SQL Server driver, but we want to ensure case-insensitive compare regardless of collation
        // Need to use Linq methods for EF
#pragma warning disable S6605, RCS1155, CA1862

        if (!string.IsNullOrWhiteSpace(request.NameSearch))
        {
            searchCriteria.Add(m => m.Name.ToLower().Contains(request.NameSearch.ToLower()));
        }

#pragma warning restore S6605, RCS1155, CA1862

        return searchCriteria.ToArray();
    }
}

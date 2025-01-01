﻿using FoodStuffs.Model.Events.ShoppingItems;
using Microsoft.AspNetCore.Mvc;
using VoidCore.AspNet.ClientApp;
using VoidCore.AspNet.Routing;
using VoidCore.Model.Functional;
using VoidCore.Model.Responses.Collections;
using VoidCore.Model.Responses.Messages;

namespace FoodStuffs.Web.Controllers.Api;

/// <summary>
/// Manage recipe shopping items.
/// </summary>
[Route(ApiRouteAttribute.BasePath + "/shopping-items")]
public class ShoppingItemsController : ControllerBase
{
    /// <summary>
    /// List shopping items. All parameters are optional and some have defaults.
    /// </summary>
    /// <param name="listHandler"></param>
    /// <param name="name">Name contains (case-insensitive)</param>
    /// <param name="isPagingEnabled">False for all results</param>
    /// <param name="page">The page of results to retrieve</param>
    /// <param name="take">How many items in a page</param>
    [HttpGet]
    [ProducesResponseType(typeof(IItemSet<ListShoppingItemsResponse>), 200)]
    [ProducesResponseType(typeof(IItemSet<IFailure>), 400)]
    public async Task<IActionResult> ListAsync([FromServices] ListShoppingItemsHandler listHandler, string? name = null, bool isPagingEnabled = true, int page = 1, int take = 30)
    {
        var request = new ListShoppingItemsRequest(
            NameSearch: name,
            IsPagingEnabled: isPagingEnabled,
            Page: page,
            Take: take);

        // Cancel long-running queries
        using var cts = new CancellationTokenSource()
            .Tee(c => c.CancelAfter(5000));

        return await listHandler
            .Handle(request, cts.Token)
            .MapAsync(HttpResponder.Respond);
    }

    /// <summary>
    /// Get a shopping item.
    /// </summary>
    /// <param name="getHandler"></param>
    /// <param name="id">The ID of the shopping item to get</param>
    [Route("{id}")]
    [HttpGet]
    [ProducesResponseType(typeof(GetShoppingItemResponse), 200)]
    [ProducesResponseType(typeof(IItemSet<IFailure>), 400)]
    public async Task<IActionResult> GetAsync([FromServices] GetShoppingItemHandler getHandler, int id)
    {
        var request = new GetShoppingItemRequest(id);

        return await getHandler
            .Handle(request)
            .MapAsync(HttpResponder.Respond);
    }

    /// <summary>
    /// Save a shopping item. Will update if found, otherwise a new item will be created.
    /// </summary>
    /// <param name="saveHandler"></param>
    /// <param name="request">The shopping item to save</param>
    [HttpPost]
    [ProducesResponseType(typeof(EntityMessage<int>), 200)]
    [ProducesResponseType(typeof(IItemSet<IFailure>), 400)]
    public async Task<IActionResult> SaveAsync([FromServices] SaveShoppingItemHandler saveHandler, [FromBody] SaveShoppingItemRequest request)
    {
        return await saveHandler
            .Handle(request)
            .MapAsync(HttpResponder.Respond);
    }

    /// <summary>
    /// Delete a shopping item.
    /// </summary>
    /// <param name="deleteHandler"></param>
    /// <param name="id">The ID of the shopping item</param>
    [Route("{id}")]
    [HttpDelete]
    [ProducesResponseType(typeof(EntityMessage<int>), 200)]
    [ProducesResponseType(typeof(IItemSet<IFailure>), 400)]
    public async Task<IActionResult> DeleteAsync([FromServices] DeleteShoppingItemHandler deleteHandler, int id)
    {
        var request = new DeleteShoppingItemRequest(id);

        return await deleteHandler
            .Handle(request)
            .MapAsync(HttpResponder.Respond);
    }
}
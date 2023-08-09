﻿using VoidCore.Model.Responses.Collections;

namespace FoodStuffs.Model.Events.Recipes;

public record ListRecipesRequest(
    string? NameSearch,
    string? CategorySearch,
    bool? IsForMealPlanning,
    string? SortBy,
    bool IsPagingEnabled,
    int Page,
    int Take) : IPaginatedRequest;

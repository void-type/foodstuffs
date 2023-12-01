﻿using VoidCore.Model.Responses.Collections;

namespace FoodStuffs.Model.Events.Categories;

public record ListCategoriesRequest(
    string? NameSearch,
    bool IsPagingEnabled,
    int Page,
    int Take) : IPaginatedRequest;

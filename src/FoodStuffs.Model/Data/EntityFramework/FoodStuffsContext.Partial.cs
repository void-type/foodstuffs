﻿using Microsoft.EntityFrameworkCore;
using VoidCore.EntityFramework;
using VoidCore.Model.Auth;
using VoidCore.Model.Time;

namespace FoodStuffs.Model.Data.EntityFramework;

public partial class FoodStuffsContext
{
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserAccessor _currentUserAccessor;

#pragma warning disable CS8618
    public FoodStuffsContext(DbContextOptions<FoodStuffsContext> options, IDateTimeService dateTimeService, ICurrentUserAccessor currentUserAccessor)
        : base(options)
    {
        _dateTimeService = dateTimeService;
        _currentUserAccessor = currentUserAccessor;

        ChangeTracker.LazyLoadingEnabled = false;
    }
#pragma warning restore CS8618

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        ChangeTracker.Entries().SetAllAuditableProperties(_dateTimeService, _currentUserAccessor.User.Login);
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        ChangeTracker.Entries().SetAllAuditableProperties(_dateTimeService, _currentUserAccessor.User.Login);
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}

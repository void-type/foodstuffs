﻿using Core.Model.Services.Time;

namespace FoodStuffs.Web.Services
{
    /// <summary>
    /// A service for getting the current UTC DateTime.
    /// </summary>
    public class UtcNowDateTimeService : IDateTimeService
    {
        /// <summary>
        /// Returns the current UTC DateTime.
        /// </summary>
        public System.DateTime Moment => System.DateTime.UtcNow;
    }
}
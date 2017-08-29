﻿using System;

namespace FoodStuffs.Model.Validation.Core
{
    public class Rule : IRule
    {
        public IValidationError ValidationError { get; }

        public bool IsValid { get; private set; } = true;

        public Rule(string fieldName, string errorMessage)
        {
            ValidationError = new ValidationError(errorMessage, fieldName);
        }

        public IRule When(Func<bool> conditionExpression)
        {
            if (IsValid)
            {
                IsValid = conditionExpression.Invoke();
            }

            return this;
        }
    }
}
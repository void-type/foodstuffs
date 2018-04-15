﻿using Core.Model.Actions.Responder;

namespace Core.Model.Actions.Steps
{
    /// <summary>
    /// Response with an item.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RespondWithItem<T> : AbstractActionStep
    {
        public RespondWithItem(T item, string logExtra = null)
        {
            _item = item;
            _logExtra = logExtra;
        }

        protected override void PerformStep(IActionResponder respond)
        {
            respond.WithItem(_item, _logExtra);
        }

        private readonly T _item;
        private readonly string _logExtra;
    }
}
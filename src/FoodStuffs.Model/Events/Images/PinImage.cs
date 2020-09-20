﻿using FoodStuffs.Model.Data;
using FoodStuffs.Model.Queries;
using System.Threading;
using System.Threading.Tasks;
using VoidCore.Domain;
using VoidCore.Domain.Events;
using VoidCore.Model.Logging;
using VoidCore.Model.Responses.Messages;

namespace FoodStuffs.Model.Events.Images
{
    public class PinImage
    {
        public class Handler : EventHandlerAbstract<Request, EntityMessage<int>>
        {
            private readonly IFoodStuffsData _data;

            public Handler(IFoodStuffsData data)
            {
                _data = data;
            }

            public override async Task<IResult<EntityMessage<int>>> Handle(Request request, CancellationToken cancellationToken = default)
            {
                return await _data.Images.Get(new ImagesByIdWithRecipesSpecification(request.Id), cancellationToken)
                    .ToResultAsync(new ImageNotFoundFailure())
                    .SelectAsync(i => i.Recipe)
                    .TeeOnSuccessAsync(r => r.PinnedImageId = request.Id)
                    .TeeOnSuccessAsync(r => _data.Recipes.Update(r, cancellationToken))
                    .SelectAsync(r => EntityMessage.Create("Image pinned.", request.Id));
            }
        }

        public class Request
        {
            public Request(int id)
            {
                Id = id;
            }

            public int Id { get; }
        }

        public class Logger : EntityMessageEventLogger<Request, int>
        {
            public Logger(ILoggingService logger) : base(logger) { }

            protected override void OnBoth(Request request, IResult<EntityMessage<int>> result)
            {
                Logger.Info($"RequestImageId: {request.Id}");

                base.OnBoth(request, result);
            }
        }
    }
}

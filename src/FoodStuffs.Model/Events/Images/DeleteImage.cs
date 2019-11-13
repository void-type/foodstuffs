using System.Threading;
using System.Threading.Tasks;
using FoodStuffs.Model.Data;
using FoodStuffs.Model.Queries;
using VoidCore.Domain;
using VoidCore.Domain.Events;
using VoidCore.Model.Logging;
using VoidCore.Model.Responses.Messages;

namespace FoodStuffs.Model.Events.Images
{
    public class DeleteImage
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
                var byId = new ImagesByIdWithBlobsSpecification(request.Id);

                return await _data.Images.Get(byId, cancellationToken)
                    .ToResultAsync(new ImageNotFoundFailure())
                    .TeeOnSuccessAsync(a => _data.Blobs.Remove(a.Blob, cancellationToken))
                    .TeeOnSuccessAsync(a => _data.Images.Remove(a, cancellationToken))
                    .SelectAsync(a => EntityMessage.Create("Image deleted.", a.Id));
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
                Logger.Info($"Id: '{request.Id}'");
                base.OnBoth(request, result);
            }
        }
    }
}
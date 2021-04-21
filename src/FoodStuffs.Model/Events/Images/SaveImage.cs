﻿using FoodStuffs.Model.Data;
using FoodStuffs.Model.Data.Models;
using FoodStuffs.Model.Data.Queries;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using VoidCore.Model.Events;
using VoidCore.Model.Functional;
using VoidCore.Model.Responses.Messages;

// Allow single file events
#pragma warning disable CA1034

namespace FoodStuffs.Model.Events.Images
{
    public static class SaveImage
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
                // Note: Size limit is controlled by the server (IIS and/or Kestrel) and validated on the client. Default is 30MB (~28.6 MiB).
                // To change this, you will need both:
                // 1. a web.config with requestLimits maxAllowedContentLength="<byte size>"
                // 2. configure FormOptions in startup for options.MultipartBodyLengthLimit = <byte size>
                // 3. edit the client-side upload validation in the recipeedit.vue file.

                var recipeResult = await _data.Recipes.Get(new RecipesByIdSpecification(request.RecipeId), cancellationToken)
                    .ToResultAsync(new RecipeNotFoundFailure()).ConfigureAwait(false);

                if (recipeResult.IsFailed)
                {
                    return Fail(recipeResult.Failures);
                }

                var image = new Image
                {
                    RecipeId = recipeResult.Value.Id
                };

                await _data.Images.Add(image, cancellationToken).ConfigureAwait(false);

                var blob = new Blob
                {
                    Id = image.Id,
                    Bytes = request.FileContent
                };

                await _data.Blobs.Add(blob, cancellationToken).ConfigureAwait(false);

                return Ok(EntityMessage.Create("Image uploaded.", image.Id));
            }
        }

        public record Request(int RecipeId, byte[] FileContent);

        public class RequestLogger : RequestLoggerAbstract<Request>
        {
            public RequestLogger(ILogger<RequestLogger> logger) : base(logger) { }

            public override void Log(Request request)
            {
                Logger.LogInformation("Requested. RecipeId: {RecipeId} FileSize: {FileSize}",
                    request.RecipeId,
                    request.FileContent.Length);
            }
        }

        public class ResponseLogger : EntityMessageEventLogger<Request, int>
        {
            public ResponseLogger(ILogger<ResponseLogger> logger) : base(logger) { }
        }

        public class Pipeline : EventPipelineAbstract<Request, EntityMessage<int>>
        {
            public Pipeline(Handler handler, RequestLogger requestLogger, ResponseLogger responseLogger)
            {
                InnerHandler = handler
                    .AddRequestLogger(requestLogger)
                    .AddPostProcessor(responseLogger);
            }
        }
    }
}

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FoodStuffs.Model.Data;
using FoodStuffs.Model.Data.Models;
using FoodStuffs.Model.Queries;
using VoidCore.Domain;
using VoidCore.Domain.Events;
using VoidCore.Model.Logging;
using VoidCore.Model.Responses.Messages;

namespace FoodStuffs.Model.Events.Recipes
{
    public class DeleteRecipe
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
                var byId = new RecipesByIdWithCategoriesAndImagesSpecification(request.Id);

                return await _data.Recipes.Get(byId, cancellationToken)
                    .ToResultAsync(new RecipeNotFoundFailure())
                    .TeeOnSuccessAsync(r => RemoveImages(r, cancellationToken))
                    .TeeOnSuccessAsync(r => _data.CategoryRecipes.RemoveRange(r.CategoryRecipe, cancellationToken))
                    .TeeOnSuccessAsync(r => _data.Recipes.Remove(r, cancellationToken))
                    .SelectAsync(r => EntityMessage.Create("Recipe deleted.", r.Id));
            }

            private async Task RemoveImages(Recipe recipe, CancellationToken cancellationToken)
            {
                var images = recipe.Image;
                // Optimization: don't bring the whole blob into RAM.
                var blobs = images.Select(i => new Blob { Id = i.Id });

                await _data.Blobs.RemoveRange(blobs, cancellationToken);
                await _data.Images.RemoveRange(images, cancellationToken);
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
                Logger.Info($"RequestId: '{request.Id}'");
                base.OnBoth(request, result);
            }
        }
    }
}

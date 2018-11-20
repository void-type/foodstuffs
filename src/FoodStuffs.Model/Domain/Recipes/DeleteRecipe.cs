using FoodStuffs.Model.Data;
using FoodStuffs.Model.Queries;
using System.Linq;
using VoidCore.Model.Domain;
using VoidCore.Model.Logging;
using VoidCore.Model.Responses.Messages;

namespace FoodStuffs.Model.Domain.Recipes
{
    public class DeleteRecipe
    {
        public class Handler : EventHandlerSyncAbstract<Request, PostSuccessUserMessage<int>>
        {
            public Handler(IFoodStuffsData data)
            {
                _data = data;
            }

            protected override Result<PostSuccessUserMessage<int>> HandleSync(Request request)
            {
                var maybeRecipe = Maybe.From(_data.Recipes.Stored
                    .WhereById(request.Id)
                    .FirstOrDefault());

                if (maybeRecipe.HasNoValue)
                {
                    return Result.Fail<PostSuccessUserMessage<int>>("Recipe not found.");
                }

                var recipeToRemove = maybeRecipe.Value;

                var categoryRecipesToRemove = _data.CategoryRecipes.Stored
                    .WhereForRecipe(recipeToRemove.Id);

                _data.CategoryRecipes.RemoveRange(categoryRecipesToRemove);
                _data.Recipes.Remove(recipeToRemove);

                _data.SaveChanges();

                return Result.Ok(PostSuccessUserMessage.Create("Recipe deleted.", request.Id));
            }

            private readonly IFoodStuffsData _data;
        }

        public class Request
        {
            public Request(int id)
            {
                Id = id;
            }

            public int Id { get; }
        }

        public class Logger : PostSuccessUserMessageEventLogger<Request, int>
        {
            public Logger(ILoggingService logger) : base(logger) { }

            public override void OnBoth(Request request, IResult<PostSuccessUserMessage<int>> result)
            {
                Logger.Info($"Id: '{request.Id}'");
                base.OnBoth(request, result);
            }
        }
    }
}

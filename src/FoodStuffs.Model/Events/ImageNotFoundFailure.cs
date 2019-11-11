using VoidCore.Domain;

namespace FoodStuffs.Model.Events
{
    public class ImageNotFoundFailure : Failure
    {
        public ImageNotFoundFailure() : base(errorMessage: "Image not found.", uiHandle: "imageId")
        {
        }
    }
}

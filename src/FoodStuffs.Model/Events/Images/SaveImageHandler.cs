﻿using FoodStuffs.Model.Data;
using FoodStuffs.Model.Data.Models;
using FoodStuffs.Model.Data.Queries;
using FoodStuffs.Model.ImageCompression;
using Microsoft.Extensions.Logging;
using VoidCore.Model.Events;
using VoidCore.Model.Functional;
using VoidCore.Model.Responses.Messages;

namespace FoodStuffs.Model.Events.Images;

public class SaveImageHandler : EventHandlerAbstract<SaveImageRequest, EntityMessage<string>>
{
    private readonly IFoodStuffsData _data;
    private readonly ILogger<SaveImageHandler> _logger;
    private readonly IImageCompressionService _compressor;

    public SaveImageHandler(IFoodStuffsData data, ILogger<SaveImageHandler> logger, IImageCompressionService imageCompressionService)
    {
        _data = data;
        _logger = logger;
        _compressor = imageCompressionService;
    }

    public override async Task<IResult<EntityMessage<string>>> Handle(SaveImageRequest request, CancellationToken cancellationToken = default)
    {
        // TODO: If we want to handle large files via streaming, we can use https://code-maze.com/aspnetcore-upload-large-files/.
        // IFormFile is still buffered by model binding.

        // Note: Size limit is controlled by the server (IIS and/or Kestrel) and validated on the client. Default is 30MB (~28.6 MiB).
        // To change this, you will need both:
        // 1. a web.config with requestLimits maxAllowedContentLength="<byte size>"
        // 2. configure FormOptions in startup for options.MultipartBodyLengthLimit = <byte size>
        // 3. edit the client-side upload validation in the RecipeEdit.vue file.

        var recipeResult = await _data.Recipes.Get(new RecipesByIdSpecification(request.RecipeId), cancellationToken)
            .ToResultAsync(new RecipeNotFoundFailure());

        if (recipeResult.IsFailed)
        {
            return Fail(recipeResult.Failures);
        }

        var compressResult = await CompressImage(request, cancellationToken);

        if (compressResult.IsFailed)
        {
            return Fail(compressResult.Failures);
        }

        var compressedFileContent = compressResult.Value;

        var image = new Image
        {
            RecipeId = recipeResult.Value.Id,
            FileName = $"{Guid.NewGuid()}.webp",
        };

        await _data.Images.Add(image, cancellationToken);

        var blob = new Blob
        {
            Id = image.Id,
            Bytes = compressedFileContent,
        };

        await _data.Blobs.Add(blob, cancellationToken);

        return Ok(EntityMessage.Create("Image uploaded.", image.FileName));
    }

    private async Task<IResult<byte[]>> CompressImage(SaveImageRequest request, CancellationToken cancellationToken)
    {
        try
        {
            using var compressedFileContent = await _compressor
                .CompressAndResizeImage(request.FileStream, 95, ResizeSettings.CenterCrop(4, 3, 1600), cancellationToken);

            return Result.Ok(compressedFileContent.ToArray());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception thrown while compressing image.");
            return Result.Fail<byte[]>(new Failure("There was an error while processing your image.", "upload-file"));
        }
    }
}

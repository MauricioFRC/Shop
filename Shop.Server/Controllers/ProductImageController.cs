using Core.Interfaces.Service;
using Core.Request;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Server.Controllers;

public class ProductImageController : BaseApiController
{
    private readonly IProductImageService _productImageService;
    private readonly IValidator<CreateProductImageRequest > _createProductImageValidator;

    public ProductImageController(
        IProductImageService productImageService, 
        IValidator<CreateProductImageRequest> createProductImageValidator
        )
    {
        _productImageService = productImageService;
        _createProductImageValidator = createProductImageValidator;
    }

    [HttpPost("upload-product-image")]
    public async Task<IActionResult> UploadProductImage(
        CreateProductImageRequest createProductImageRequest, 
        CancellationToken cancellationToken)
    {
        var result = await _createProductImageValidator.ValidateAsync(createProductImageRequest, cancellationToken);
        if (!result.IsValid) return BadRequest(result.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }));

        return Ok(await _productImageService.UploadProductImage(createProductImageRequest, cancellationToken));
    }

    [HttpGet("get-product-image/{productId}")]
    public async Task<IActionResult> GetProductImage([FromRoute] int productId, CancellationToken cancellationToken)
    {
        return File(await _productImageService.GetProductImage(productId, cancellationToken), "image/png");
    }

    [HttpDelete("delete-product-image/{imageId}")]
    public async Task<IActionResult> DeleteProductImage([FromRoute] int imageId, CancellationToken cancellationToken)
    {
        return Ok(await _productImageService.DeleteProductImage(imageId, cancellationToken));
    }

    [HttpGet("get-all-products-images-ids")]
    public async Task<IActionResult> GetAllImagesIds()
    {
        return Ok(await _productImageService.GetAllImagesId());
    }
}

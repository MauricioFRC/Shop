using Core.DTOs;
using Core.Interfaces.Service;
using FluentValidation;
using Infrastructure.Validations;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Server.Controllers;

public class ProductController : BaseApiController
{
    private readonly IProductService _productService;
    private readonly IValidator<CreateProductDTO> _productValidation;
    private readonly IValidator<UpdateProductDTO> _updateProductValidation;

    public ProductController(
        IProductService productService,
        IValidator<CreateProductDTO> productValidation,
        IValidator<UpdateProductDTO> updateProductValidation
        )
    {
        _productService = productService;
        _productValidation = productValidation;
        _updateProductValidation = updateProductValidation;
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetAllProducts()
    {
        return Ok(await _productService.GetAllProducts());
    }

    [HttpGet("product/{id}")]
    public async Task<IActionResult> GetProductById([FromRoute] int id)
    {
        return Ok(await _productService.GetProductById(id));
    }

    [HttpPost("create-product")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO createProductDTO)
    {
        var result = await _productValidation.ValidateAsync(createProductDTO);
        if (!result.IsValid)
            return BadRequest(result.Errors.Select(e => new
            {
                e.PropertyName,
                e.ErrorMessage
            }));

        return Ok(await _productService.CreateProduct(createProductDTO));
    }

    [HttpPut("update-product/{id}")]
    public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductDTO updateProductDTO)
    {
        var result = await _updateProductValidation.ValidateAsync(updateProductDTO);
        if (!result.IsValid)
            return BadRequest(result.Errors.Select(e => new
            {
                e.PropertyName,
                e.ErrorMessage
            }));

        return Ok(await _productService.UpdateProduct(id, updateProductDTO));
    }

    [HttpDelete("delete-product/{id}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] int id)
    {
        return Ok(await _productService.DeleteProduct(id));
    }
}

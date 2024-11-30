using Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Server.Controllers;

public class ProductController : BaseApiController
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetAllProducts()
    {
        return Ok(await _productService.GetAllProducts());
    }

    [HttpGet("product/{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        return Ok();
    }

    [HttpPost("create-product")]
    public async Task<IActionResult> CreateProduct()
    {
        return Ok();
    }
}

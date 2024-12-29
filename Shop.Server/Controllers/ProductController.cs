using Core.DTOs.Product;
using Core.Interfaces.Service;
using Core.Request;
using FluentValidation;
using Infrastructure.Validations;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Server.Controllers;

public class ProductController : BaseApiController
{
    private readonly IProductService _productService;
    private readonly IValidator<CreateProductRequest> _createProductValidation;
    private readonly IValidator<UpdateProductDTO> _updateProductValidation;

    public ProductController(
        IProductService productService,
        IValidator<CreateProductRequest> createProductValidation,
        IValidator<UpdateProductDTO> updateProductValidation
        )
    {
        _productService = productService;
        _createProductValidation = createProductValidation;
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
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest createProductRequest)
    {
        var result = await _createProductValidation.ValidateAsync(createProductRequest);
        if (!result.IsValid) return BadRequest(result.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));

        return Ok(await _productService.CreateProduct(createProductRequest));
    }

    [HttpPut("update-product/{id}")]
    public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductDTO updateProductDTO)
    {
        var result = await _updateProductValidation.ValidateAsync(updateProductDTO);
        if (!result.IsValid) return BadRequest(result.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));

        return Ok(await _productService.UpdateProduct(id, updateProductDTO));
    }

    [HttpDelete("delete-product/{id}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] int id)
    {
        return Ok(await _productService.DeleteProduct(id));
    }

    [HttpGet("product-qr-generator/{productId}")]
    public async Task<IActionResult> ProductQrGenerator(int productId)
    {
        return File(await _productService.GenerateProductDescriptionQr(productId), "image/png");
    }

    [HttpGet("product-pdf-report-generator/{maxRange}")]
    public async Task<IActionResult> ProductPdfReportGenerator(int maxRange, CancellationToken cancellationToken, string fileName = "reporte_pdf")
    {
        var pdfReport = await _productService.GeneratePdfProductReport(maxRange, cancellationToken);
        return File(pdfReport, "application/pdf", fileName + "_" + DateTime.UtcNow.ToShortDateString());
    }
}

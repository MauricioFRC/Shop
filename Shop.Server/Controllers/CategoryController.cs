using Core.DTOs.Category;
using Core.Interfaces.Service;
using Core.Request;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Server.Controllers;

public class CategoryController : BaseApiController
{
    private readonly ICategoryService _categoryService;
    private readonly IValidator<CreateCategoryRequest> _createCategoryValidator;
    private readonly IValidator<UpdateCategoryDTO> _updateCategoryValidator;

    public CategoryController(
        ICategoryService categoryService,
        IValidator<CreateCategoryRequest> createCategoryValidator,
        IValidator<UpdateCategoryDTO> updateCategoryValidator
        )
    {
        _categoryService = categoryService;
        _createCategoryValidator = createCategoryValidator;
        _updateCategoryValidator = updateCategoryValidator;
    }

    [HttpPost("create-category")]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest createCategoryRequest)
    {
        var result = await _createCategoryValidator.ValidateAsync(createCategoryRequest);
        if (!result.IsValid) return BadRequest(result.Errors);

        return Ok(await _categoryService.CreateCategory(createCategoryRequest));
    }

    [HttpPut("update-category/{id}")]
    public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] UpdateCategoryDTO updateCategoryDTO)
    {
        var result = await _updateCategoryValidator.ValidateAsync(updateCategoryDTO);
        if (!result.IsValid) return BadRequest(result.Errors);

        return Ok(await _categoryService.UpdateCategory(id, updateCategoryDTO));
    }

    [HttpDelete("delete-category/{id}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] int id)
    {
        return Ok(await _categoryService.DeleteCategory(id));
    }

    [HttpGet("search-category/{id}")]
    public async Task<IActionResult> SearchCategory([FromRoute] int id)
    {
        return Ok(await _categoryService.SearchCategory(id));
    }

    [HttpGet("list-categories")]
    public async Task<IActionResult> ListCategories()
    {
        return Ok(await _categoryService.ListCategories());
    }
}

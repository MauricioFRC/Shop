using Core.DTOs.Category;
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Request;
using Infrastructure.Context;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AplicationDbContext _context;

    public CategoryRepository(AplicationDbContext context)
    {
        _context = context;
    }

    public IReadOnlyList<string> GetCategoriesName()
    {
        return _context.Categories.Select(c => c.Name).ToList().AsReadOnly();
    }

    public async Task<CategoryResponseDTO> CreateCategory(CreateCategoryRequest createCategoryRequest)
    {
        var category = createCategoryRequest.Adapt<Category>();

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return category.Adapt<CategoryResponseDTO>();
    }

    public async Task<CategoryResponseDTO> UpdateCategory(int id, UpdateCategoryDTO updateCategoryRequest)
    {
        var updatedCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

        updateCategoryRequest.Adapt(updatedCategory);

        _context.Categories.Update(updatedCategory);
        await _context.SaveChangesAsync();

        return updatedCategory.Adapt<CategoryResponseDTO>();
    }

    public async Task<CategoryResponseDTO> DeleteCategory(int id)
    {
        var deletedCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

        _context.Categories.Remove(deletedCategory);
        await _context.SaveChangesAsync();

        return deletedCategory.Adapt<CategoryResponseDTO>();
    }

    public async Task<CategoryResponseDTO> SearchCategory(int id)
    {
        var searchCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        return searchCategory.Adapt<CategoryResponseDTO>();
    }

    public async Task<List<CategoryResponseDTO>> ListCategories()
    {
        var categoryList = await _context.Categories.ToListAsync();
        return categoryList.Adapt<List<CategoryResponseDTO>>();
    }
}

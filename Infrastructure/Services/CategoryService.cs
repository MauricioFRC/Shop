using Core.DTOs.Category;
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Core.Request;
using Mapster;

namespace Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public IReadOnlyList<string> GetCategoriesName()
    {
        var categories = _categoryRepository.GetCategoriesName().ToList().AsReadOnly();
        if (categories.Count != 0)
            throw new Exception("La lista no contiene ninguna categoria");

        return categories;
    }

    public async Task<CategoryResponseDTO> CreateCategory(CreateCategoryRequest createCategoryRequest)
    {
        var createCategory = createCategoryRequest.Adapt<Category>();

        await _categoryRepository.CreateCategory(createCategoryRequest);

        return createCategory == null 
            ? throw new Exception("No se pudo crear la categoria.") 
            : createCategory.Adapt<CategoryResponseDTO>();
    }

    public async Task<CategoryResponseDTO> UpdateCategory(int id, UpdateCategoryDTO updateCategoryDTO)
    {
        var updatedCategory = await _categoryRepository.UpdateCategory(id, updateCategoryDTO);
        return updatedCategory ?? throw new Exception("No se encontró el id");
    }

    public async Task<CategoryResponseDTO> DeleteCategory(int id)
    {
        ValidateId(id);
        var deletedCategory = await _categoryRepository.DeleteCategory(id);
        return deletedCategory ?? throw new Exception($"No se encontró la categoria con el id {id}.");
    }

    public async Task<CategoryResponseDTO> SearchCategory(int id)
    {
        ValidateId(id);
        var searchCategory = await _categoryRepository.SearchCategory(id);
        return searchCategory ?? throw new Exception($"No se encontró la categoria con el id: {id}.");
    }

    public async Task<List<CategoryResponseDTO>> ListCategories()
    {
        var categoryList = await _categoryRepository.ListCategories();
        return categoryList ?? throw new Exception("La lista esta vacia.");
    }

    private static void ValidateId(int id)
    {
        if (id <= 0) throw new Exception("El id no puede ser negativo");
    }
}

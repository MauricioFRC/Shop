using Core.DTOs.Category;
using Core.Request;

namespace Core.Interfaces.Repository;

public interface ICategoryRepository
{
    public IReadOnlyList<string> GetCategoriesName();
    public Task<CategoryResponseDTO> CreateCategory(CreateCategoryRequest createCategoryRequest);
    public Task<CategoryResponseDTO> UpdateCategory(int id, UpdateCategoryDTO updateCategoryRequest);
    public Task<CategoryResponseDTO> DeleteCategory(int id);
    public Task<CategoryResponseDTO> SearchCategory(int id);
    public Task<List<CategoryResponseDTO>> ListCategories();
}

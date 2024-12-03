using Core.DTOs.Product;
using Core.Entities;
using Core.Request;

namespace Core.Interfaces.Repository;

public interface IProductRepository
{
    public Task<List<ProductResponseDTO>> GetAllProducts();
    public Task<ProductResponseDTO> GetProductById(int id);
    public Task<ProductResponseDTO> CreateProduct(CreateProductRequest createProductRequest);
    public Task<ProductResponseDTO> UpdateProduct(int id, UpdateProductDTO updateProductDTO);
    public Task<ProductResponseDTO> DeleteProduct(int id);
    
    public Task<Category> GetCategories(string categoryName);
}

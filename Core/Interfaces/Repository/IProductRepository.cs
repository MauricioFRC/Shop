using Core.DTOs.Product;
using Core.Entities;
using Core.Request;
using Microsoft.AspNetCore.Http;

namespace Core.Interfaces.Repository;

public interface IProductRepository
{
    public Task<List<ProductResponseDTO>> GetAllProducts();
    public Task<ProductResponseDTO> GetProductById(int id);
    public Task<ProductResponseDTO> CreateProduct(CreateProductRequest createProductRequest);
    public Task<ProductResponseDTO> UpdateProduct(int id, UpdateProductDTO updateProductDTO);
    public Task<ProductResponseDTO> DeleteProduct(int id);
    // public Task<ProductResponseDTO> UploadProductImage(int productId, IFormFile file, CancellationToken cancellationToken);
    // public Task<byte[]> GetProductImage(int productId, CancellationToken cancellationToken);

    public Task<Category> GetCategories(string categoryName);
    public Task<List<ProductResponseDTO>> GetProductsByRange(int range, CancellationToken cancellationToken);
}

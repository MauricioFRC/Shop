using Core.DTOs.Product;
using Core.Request;

namespace Core.Interfaces.Service;

public interface IProductService
{
    #region Crud
    public Task<List<ProductResponseDTO>> GetAllProducts();
    public Task<ProductResponseDTO> GetProductById(int id);
    public Task<ProductResponseDTO> CreateProduct(CreateProductRequest createProductRequest);
    public Task<ProductResponseDTO> UpdateProduct(int id, UpdateProductDTO updateProductDTO);
    public Task<ProductResponseDTO> DeleteProduct(int id);
    #endregion

    #region Image Generator
    public Task<byte[]> GenerateProductDescriptionQr(int productId);
    #endregion
}

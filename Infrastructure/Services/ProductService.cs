using Core.DTOs;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Core.Request;
using Mapster;

namespace Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductResponseDTO> CreateProduct(CreateProductRequest createProductRequest)
    {
        var createProduct = await _productRepository.CreateProduct(createProductRequest);
        return createProduct == null ? throw new Exception("No se pudo crear el producto.") : createProduct.Adapt<ProductResponseDTO>();
    }

    public async Task<ProductResponseDTO> DeleteProduct(int id)
    {
        var deleteProduct = await _productRepository.DeleteProduct(id);

        return deleteProduct == null ? throw new Exception("No se encontró el producto") : deleteProduct.Adapt<ProductResponseDTO>();
    }

    public async Task<List<ProductResponseDTO>> GetAllProducts()
    {
        var products = await _productRepository.GetAllProducts();
        return products == null ? throw new Exception("No hay ningun producto disponible") : products.Adapt<List<ProductResponseDTO>>();
    }

    public async Task<ProductResponseDTO> GetProductById(int id)
    {
        var getProduct = await _productRepository.GetProductById(id);

        return getProduct ?? throw new Exception("No se encontró el Id solicitado");
    }

    public async Task<ProductResponseDTO> UpdateProduct(int id, UpdateProductDTO updateProductDTO)
    {
        var updateProduct = await _productRepository.UpdateProduct(id, updateProductDTO);

        return updateProduct ?? throw new Exception("No se encontró el producto solicitado");
    }
}

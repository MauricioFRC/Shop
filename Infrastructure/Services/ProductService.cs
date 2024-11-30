using Core.DTOs;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Mapster;

namespace Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<ProductResponseDTO>> GetAllProducts()
    {
        var products = await _productRepository.GetAllProducts();
        return products == null ? throw new Exception("No hay ningun producto disponible") : products.Adapt<List<ProductResponseDTO>>();
    }
}

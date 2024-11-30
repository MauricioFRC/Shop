using Core.DTOs;

namespace Core.Interfaces.Repository;

public interface IProductRepository
{
    public Task<List<ProductResponseDTO>> GetAllProducts();
}

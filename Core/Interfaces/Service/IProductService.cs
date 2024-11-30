using Core.DTOs;

namespace Core.Interfaces.Service;

public interface IProductService
{
    public Task<List<ProductResponseDTO>> GetAllProducts();
}

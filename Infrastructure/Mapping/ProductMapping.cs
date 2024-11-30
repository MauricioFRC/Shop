using Core.DTOs;
using Core.Entities;
using Mapster;

namespace Infrastructure.Mapping;

public class ProductMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Product, ProductResponseDTO>()
            .Map(dest => dest.Name, src => src.ProductName)
            .Map(dest => dest.Price, src => src.Price)
            .Map(dest => dest.Description, src => src.ProductDescription)
            .Map(dest => dest.Stock, src => src.Stock);
    }
}

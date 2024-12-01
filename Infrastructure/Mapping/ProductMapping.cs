using Core.DTOs;
using Core.Entities;
using Mapster;

namespace Infrastructure.Mapping;

public class ProductMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Product, ProductResponseDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.ProductName)
            .Map(dest => dest.Price, src => src.Price)
            .Map(dest => dest.Description, src => src.ProductDescription)
            .Map(dest => dest.Stock, src => src.Stock);

        config.NewConfig<CreateProductDTO, Product>()
            .Map(dest => dest.Price, src => src.Price)
            .Map(dest => dest.ProductName, src => src.Name)
            .Map(dest => dest.ProductDescription, src => src.Description)
            .Map(dest => dest.Stock, src => src.Stock)
            .Map(dest => dest.Category, src => src.Category ?? "Has No Category");

        config.NewConfig<UpdateProductDTO, Product>()
            .Map(dest => dest.Price, src => src.Price)
            .Map(dest => dest.ProductName, src => src.Name)
            .Map(dest => dest.ProductDescription, src => src.Description)
            .Map(dest => dest.Stock, src => src.Stock)
            .Map(dest => dest.Category, src => src.Category);
    }
}

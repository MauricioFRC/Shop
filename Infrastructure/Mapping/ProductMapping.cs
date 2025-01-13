using Core.DTOs.Product;
using Core.Entities;
using Core.Request;
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
            .Map(dest => dest.Stock, src => src.Stock)
            .Map(dest => dest.Category, src => src.Category.Name);
            // .Map(dest => dest.HasImage = true, src => src.ProductImage != null);

        config.NewConfig<CreateProductRequest, Product>()
            .Map(dest => dest.Price, src => src.Price)
            .Map(dest => dest.ProductName, src => src.Name)
            .Map(dest => dest.ProductDescription, src => src.Description)
            .Map(dest => dest.Stock, src => src.Stock);

        config.NewConfig<UpdateProductDTO, Product>()
            .Map(dest => dest.Price, src => src.Price)
            .Map(dest => dest.ProductName, src => src.Name)
            .Map(dest => dest.ProductDescription, src => src.Description)
            .Map(dest => dest.Stock, src => src.Stock)
            .Map(dest => dest.Category, src => src.Category);

        config.NewConfig<CreateProductRequestImg, Product>()
            .Map(dest => dest.Price, src => src.Price)
            .Map(dest => dest.ProductName, src => src.ProductName)
            .Map(dest => dest.ProductDescription, src => src.ProductDescription)
            .Map(dest => dest.Stock, src => src.Stock)
            .Map(dest => dest.ProductImage, src => src.ProductImage);
    }
}

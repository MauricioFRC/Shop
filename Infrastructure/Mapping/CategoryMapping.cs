using Core.DTOs.Category;
using Core.Entities;
using Core.Request;
using Mapster;

namespace Infrastructure.Mapping;

public class CategoryMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Category, CategoryResponseDTO>()
            .Map(dest => dest.CategoryId, src => src.Id)
            .Map(dest => dest.CategoryName, src => src.Name);

        config.NewConfig<CreateCategoryRequest, Category>()
            .Map(dest => dest.Name, src => src.CategoryName);

        config.NewConfig<UpdateCategoryDTO, Category>()
            .Map(dest => dest.Name, src => src.CategoryName);
    }
}

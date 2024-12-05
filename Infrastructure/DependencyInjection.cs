using Core.DTOs.Category;
using Core.DTOs.Order;
using Core.DTOs.OrderDetail;
using Core.DTOs.Product;
using Core.DTOs.User;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Core.Request;
using FluentValidation;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.Validations.Category;
using Infrastructure.Validations.Order;
using Infrastructure.Validations.OrderDetail;
using Infrastructure.Validations.Product;
using Infrastructure.Validations.User;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration
        )
    {
        services.AddDatabase(configuration);
        services.AddMapping();
        services.AddRepositories();
        services.AddServices();
        services.AddValidations();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Shop");

        services.AddDbContext<AplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRespository, UserRepository>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IOrderDetailService, OrderDetailService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }

    private static IServiceCollection AddValidations(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateCategoryRequest>, CreateCategoryValidation>();
        services.AddScoped<IValidator<CreateOrderDetailRequest>, CreateOrderDetailValidation>();
        services.AddScoped<IValidator<CreateOrderRequest>, CreateOrderValidation>();
        services.AddScoped<IValidator<CreateProductRequest>, CreateProductValidation>();
        services.AddScoped<IValidator<CreateUserRequest>, CreateUserValidation>();
        services.AddScoped<IValidator<UpdateCategoryDTO>, UpdateCategoryValidation>();
        services.AddScoped<IValidator<UpdateOrderDetailDTO>, UpdateOrderDetailValidation>();
        services.AddScoped<IValidator<UpdateOrderDTO>, UpdateOrderValidation>();
        services.AddScoped<IValidator<UpdateProductDTO>, UpdateProductValidation>();
        services.AddScoped<IValidator<UserUpdateDTO>, UserUpdateValidation>();

        return services;
    }

    private static IServiceCollection AddMapping(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}

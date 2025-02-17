﻿using Core.DTOs.Category;
using Core.DTOs.Order;
using Core.DTOs.OrderDetail;
using Core.DTOs.Payment;
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
using Infrastructure.Validations.Payment;
using Infrastructure.Validations.Product;
using Infrastructure.Validations.Product.ProductImage;
using Infrastructure.Validations.Ticket;
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
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRespository, UserRepository>();
        services.AddScoped<IProductImageRepository, ProductImageRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IOrderDetailService, OrderDetailService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IProductImageService, ProductImageService>();
        services.AddScoped<ITicketService, TicketService>();

        return services;
    }

    private static IServiceCollection AddValidations(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateCategoryRequest>, CreateCategoryValidation>();
        services.AddScoped<IValidator<CreateOrderDetailRequest>, CreateOrderDetailValidation>();
        services.AddScoped<IValidator<CreateOrderRequest>, CreateOrderValidation>();
        services.AddScoped<IValidator<CreatePaymentRequest>, CreatePaymentValidation>();
        services.AddScoped<IValidator<CreateProductRequest>, CreateProductValidation>();
        services.AddScoped<IValidator<CreateUserRequest>, CreateUserValidation>();
        services.AddScoped<IValidator<UpdateCategoryDTO>, UpdateCategoryValidation>();
        services.AddScoped<IValidator<UpdateOrderDetailDTO>, UpdateOrderDetailValidation>();
        services.AddScoped<IValidator<UpdateOrderDTO>, UpdateOrderValidation>();
        services.AddScoped<IValidator<UpdatePaymentDTO>, UpdatePaymentValidation>();
        services.AddScoped<IValidator<UpdateProductDTO>, UpdateProductValidation>();
        services.AddScoped<IValidator<UserUpdateDTO>, UserUpdateValidation>();
        services.AddScoped<IValidator<CreateProductRequestImg>, CreateProductImgValidation>();
        services.AddScoped<IValidator<CreateProductImageRequest>, CreateProductImageValidation>();
        services.AddScoped<IValidator<UpdateUserRoleDto>, UpdateUserRoleValidation>();
        services.AddScoped<IValidator<CreateTicketRequest>, CreateTicketValidation>();

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

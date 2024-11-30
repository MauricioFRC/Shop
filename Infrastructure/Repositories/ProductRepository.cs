using Core.DTOs;
using Core.Entities;
using Core.Interfaces.Repository;
using Infrastructure.Context;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AplicationDbContext _context;

    public ProductRepository(AplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductResponseDTO>> GetAllProducts()
    {
        var products = await _context.Products.ToListAsync();
        return products.Adapt<List<ProductResponseDTO>>();
    }
}

using Core.DTOs.Product;
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Request;
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

    public async Task<Category> GetCategories(string categoryName)
    {
        var searchCategory = await _context.Categories
            .FirstOrDefaultAsync(x => x.Name == categoryName);

        return searchCategory!;
    }

    public async Task<ProductResponseDTO> CreateProduct(CreateProductRequest createProductRequest)
    {
        var entity = createProductRequest.Adapt<Product>();
        
        _context.Products.Add(entity);
        await _context.SaveChangesAsync();

        return entity.Adapt<ProductResponseDTO>();
    }

    public async Task<ProductResponseDTO> DeleteProduct(int id)
    {
        var deletedProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        
        _context.Products.Remove(deletedProduct!);
        await _context.SaveChangesAsync();

        return deletedProduct.Adapt<ProductResponseDTO>();
    }

    public async Task<List<ProductResponseDTO>> GetAllProducts()
    {
        var products = await _context.Products.ToListAsync();
        return products.Adapt<List<ProductResponseDTO>>();
    }

    public async Task<ProductResponseDTO> GetProductById(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        return product.Adapt<ProductResponseDTO>();
    }

    public async Task<ProductResponseDTO> UpdateProduct(int id, UpdateProductDTO updateProductDTO)
    {
        var updateProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

        updateProductDTO.Adapt(updateProduct);
        _context.Products.Update(updateProduct!);

        await _context.SaveChangesAsync();

        return updateProduct.Adapt<ProductResponseDTO>();
    }

}

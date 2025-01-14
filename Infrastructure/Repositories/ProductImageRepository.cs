using Core.DTOs.Product.ProductImage;
using Core.Entities;
using Core.Interfaces.Repository;
using Core.Request;
using Infrastructure.Context;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductImageRepository : IProductImageRepository
{
    private readonly AplicationDbContext _context;

    public ProductImageRepository(AplicationDbContext context)
    {
        _context = context;
    }

    public async Task<byte[]> GetProductImage(int productId, CancellationToken cancellationToken)
    {
        var productImage = await _context.ProductImages
            .FirstOrDefaultAsync(x => x.ProductId == productId, cancellationToken)
            ?? throw new ArgumentNullException($"El producto con Id: {productId} no tiene ninguna imagen");

        return productImage!.Image;
    }

    public async Task<IEnumerable<byte[]>> GetAllProductsImageByProductId(int productId, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .Include(x => x.ProductImages)
            .FirstOrDefaultAsync(x => x.ProductImageId == productId, cancellationToken)
            ?? throw new ArgumentNullException($"No se encontraron imagenes para el producto: {productId}");

        return product.ProductImages.Select(x => x.Image);
    }

    public async Task<ProductImageResponseDTO> UploadProductImage(CreateProductImageRequest createProductImageRequest, CancellationToken cancellationToken)
    {
        var uploadProductImage = createProductImageRequest.Adapt<ProductImage>();

        using var stream = new MemoryStream();
        await createProductImageRequest.File.CopyToAsync(stream, cancellationToken);

        uploadProductImage.Image = stream.ToArray();

        await _context.ProductImages.AddAsync(uploadProductImage, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return uploadProductImage.Adapt<ProductImageResponseDTO>();
    }
}

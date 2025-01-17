using Core.DTOs.Product.ProductImage;
using Core.Interfaces.Repository;
using Core.Interfaces.Service;
using Core.Request;

namespace Infrastructure.Services;

public class ProductImageService : IProductImageService
{
    private readonly IProductImageRepository _productImageRepository;

    public ProductImageService(IProductImageRepository productImageRepository)
    {
        _productImageRepository = productImageRepository;
    }

    public async Task<IEnumerable<byte[]>> GetAllProductsImageByProductId(int productId, CancellationToken cancellationToken)
    {
        ValidateId(productId);
        var productImages = await _productImageRepository.GetAllProductsImageByProductId(productId, cancellationToken)
            ?? throw new ArgumentNullException($"No se pudo obtener las imagenes del producto con el Id: {productId}");

        return productImages;
    }

    public async Task<byte[]> GetProductImage(int productId, CancellationToken cancellationToken)
    {
        var productImage = await _productImageRepository.GetProductImage(productId, cancellationToken)
            ?? throw new ArgumentNullException($"No se pudo obtener las imagenes del producto con el Id: {productId}");

        return productImage;
    }

    public async Task<ProductImageResponseDTO> UploadProductImage(CreateProductImageRequest createProductImageRequest, CancellationToken cancellationToken)
    {
        var uploadProductImage = await _productImageRepository.UploadProductImage(createProductImageRequest, cancellationToken)
            ?? throw new ArgumentNullException("No se pudo subir la imagen del producto");

        return uploadProductImage;
    }

    public async Task<ProductImageResponseDTO> DeleteProductImage(int id, CancellationToken cancellationToken)
    {
        ValidateId(id);
        var deletedImage = await _productImageRepository.DeleteProductImage(id, cancellationToken)
            ?? throw new ArgumentNullException($"No se encontró la imagen con el id {id}");

        return deletedImage;
    }

    public async Task<List<ProductImageResponseDTO>> GetAllImagesId()
    {
        var imagesIds = await _productImageRepository.GetAllImagesId()
            ?? throw new ArgumentNullException("No se encontraron imagenes");

        return imagesIds;
    }

    private void ValidateId(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);
    }
}

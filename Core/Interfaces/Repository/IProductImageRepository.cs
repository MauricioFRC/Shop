using Core.DTOs.Product.ProductImage;
using Core.Request;

namespace Core.Interfaces.Repository;

public interface IProductImageRepository
{
    public Task<ProductImageResponseDTO> UploadProductImage(CreateProductImageRequest createProductImageRequest, CancellationToken cancellationToken);
    public Task<byte[]> GetProductImage(int productId, CancellationToken cancellationToken);
    public Task<IEnumerable<byte[]>> GetAllProductsImageByProductId(int productId, CancellationToken cancellationToken);
    public Task<ProductImageResponseDTO> DeleteProductImage(int id, CancellationToken cancellationToken);
    public Task<List<ProductImageResponseDTO>> GetAllImagesId();
}

using Microsoft.AspNetCore.Http;

namespace Core.DTOs.Product.ProductImage;

public class UploadProductImages
{
    public int ProductId { get; set; }
    public IEnumerable<IFormFile> ImageFile { get; set; } = [];
}

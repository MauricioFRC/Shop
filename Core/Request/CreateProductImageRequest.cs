using Microsoft.AspNetCore.Http;

namespace Core.Request;

public class CreateProductImageRequest 
{
    public int ProductId { get; set; }
    public IFormFile File { get; set; } = null!;
}

namespace Core.Request;

public class CreateProductRequestImg
{
    public decimal Price { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? ProductDescription { get; set; } = string.Empty;
    public int Stock { get; set; }
    public byte[] ProductImage { get; set; } = [];
}

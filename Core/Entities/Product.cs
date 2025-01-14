namespace Core.Entities;

public class Product
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? ProductDescription { get; set; } = string.Empty;
    public int Stock { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public int ProductImageId { get; set; }
    public List<ProductImage> ProductImages { get; set; } = [];
}

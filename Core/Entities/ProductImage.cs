namespace Core.Entities;

public class ProductImage 
{
    public int Id { get; set; }
    public byte[] Image { get; set; } = [];

    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
}

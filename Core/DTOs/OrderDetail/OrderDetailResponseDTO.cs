namespace Core.DTOs.OrderDetail;

public class OrderDetailResponseDTO
{
    public int OrderDetailId { get; set; }
    public int OrderId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string UserName { get; set; } = string.Empty;
}

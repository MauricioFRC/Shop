namespace Core.DTOs.OrderDetail;

public class UpdateOrderDetailDTO
{
    public int OrderId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

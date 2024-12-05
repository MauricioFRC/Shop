namespace Core.Request;

public class CreateOrderDetailRequest
{
    public int OrderId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

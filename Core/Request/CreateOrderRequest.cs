namespace Core.Request;

public class CreateOrderRequest
{
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public int UserId { get; set; }
}

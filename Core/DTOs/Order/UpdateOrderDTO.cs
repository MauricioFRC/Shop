namespace Core.DTOs.Order;

public class UpdateOrderDTO
{
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public int UserId { get; set; }
}

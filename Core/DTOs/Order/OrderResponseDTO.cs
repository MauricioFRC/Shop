namespace Core.DTOs.Order;

public class OrderResponseDTO
{
    public int OrderId { get; set; }
    public string OrderDate { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public int UserId { get; set; }
}

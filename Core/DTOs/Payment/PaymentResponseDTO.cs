namespace Core.DTOs.Payment;

public class PaymentResponseDTO
{
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public int OrderId { get; set; }
    public string PaymentDate { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public int UserId { get; set; }
}

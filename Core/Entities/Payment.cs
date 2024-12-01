namespace Core.Entities;

public class Payment
{
    public int Id { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;

    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
}
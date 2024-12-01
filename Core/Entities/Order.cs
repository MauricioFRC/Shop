namespace Core.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;

    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public Payment Payment { get; set; } = null!;
    public List<OrderDetail> OrderDetails { get; set; } = [];
}

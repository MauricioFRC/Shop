﻿namespace Core.Request;

public class CreatePaymentRequest
{
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public int OrderId { get; set; }
    public DateTime PaymentDate { get; set; }
}

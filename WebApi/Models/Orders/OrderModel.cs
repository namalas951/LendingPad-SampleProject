using System;

public class OrderModel
{
    public Guid ProductId { get; set; }
    public DateTime? DateOfPurchase { get; set; }
    public int? Quantity { get; set; }
    public decimal? TotalPrice { get; set; }
}   
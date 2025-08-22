namespace SpamMusubiAPI.DTOs;

public class OrderDto
{
    public int OrderId { get; set; }
    public string? Batch { get; set; }
    public string? NameCustomer { get; set; }
    public int MenuOrder { get; set; }
    public int? AddOns { get; set; }
    public int Quantity { get; set; }
    public string? CareOf { get; set; }
    public int PaymentScheme { get; set; }
    public string Status { get; set; } = "Pending";
    public DateTime? DateDelivered { get; set; }
    public DateTime? DatePaid { get; set; }
    public DateTime CreatedAt { get; set; }
}

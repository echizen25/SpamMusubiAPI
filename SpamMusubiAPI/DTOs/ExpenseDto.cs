namespace SpamMusubiAPI.DTOs;

public class ExpenseDto
{
    public int ExpenseId { get; set; }
    public DateTime ExpenseDate { get; set; }
    public string Category { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Amount { get; set; }
}

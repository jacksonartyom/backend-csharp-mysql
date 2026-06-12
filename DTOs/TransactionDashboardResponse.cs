public class TransactionDashboardResponse
{
    public string TransactionId { get; set; } = string.Empty;
    public string WalletId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Note { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string? TransactionDate { get; set; }
    public string? CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public string? UserId { get; set; }
}
public class CreateTransactionDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Note { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string TransactionDate { get; set; }
    public string WalletId { get; set; } = string.Empty;
    public string CategoryId { get; set; } = string.Empty;
}
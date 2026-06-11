public class CreateTransactionDto
{
    public string name { get; set; } = string.Empty;
    public decimal amount { get; set; }
    public string type { get; set; } = string.Empty;
    public DateTime transactionDate { get; set; }
}
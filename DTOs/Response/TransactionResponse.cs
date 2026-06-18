using System.Text.Json.Serialization;
public class TransactionResponse
{
    public string TransactionId { get; set; } = string.Empty;
    public string WalletId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Note { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    [JsonPropertyName("transaction_date")]
    public string? TransactionDate { get; set; }
    public CategoryResponse? Category { get; set; }
    public string? UserId { get; set; }
}
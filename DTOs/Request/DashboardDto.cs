using System.Text.Json.Serialization;

public class DashboardDto
{
    [JsonPropertyName("total_balance")]
    public decimal TotalBalance { get; set; }

    public List<Wallet>? Wallets { get; set; }

    [JsonPropertyName("recent_transactions")]
    public List<TransactionResponse>? RecentTransactions { get; set; }
}
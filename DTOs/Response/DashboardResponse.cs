using System.Text.Json.Serialization;

public class DashboardResponse
{
    [JsonPropertyName("total_balance")]
    public decimal TotalBalance { get; set; }

    public List<WalletResponse>? Wallets { get; set; }

    [JsonPropertyName("recent_transactions")]
    public List<TransactionDetailResponse>? RecentTransactions { get; set; }
}
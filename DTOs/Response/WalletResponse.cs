using System.Text.Json.Serialization;

public class WalletResponse
{
    [JsonPropertyName("_id")]
    public required string WalletId { get; set; }
    [JsonPropertyName("wallet_name")]
    public string WalletName { get; set; } = string.Empty;
    [JsonPropertyName("wallet_detail")]
    public string WalletDetail { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public string UserId { get; set; } = string.Empty;

}
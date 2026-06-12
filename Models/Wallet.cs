using System.ComponentModel.DataAnnotations.Schema;

[Table("wallets")]
public class Wallet
{
    [Column("id")]
    public int Id { get; set; }

    [Column("wallet_id")]
    public string WalletId { get; set; } = string.Empty;

    [Column("wallet_name")]
    public string WalletName { get; set; } = string.Empty;

    [Column("wallet_detail")]
    public string WalletDetail { get; set; } = string.Empty;


    [Column("balance")]
    public decimal Balance { get; set; }

    [Column("user_id")]
    public string UserId { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
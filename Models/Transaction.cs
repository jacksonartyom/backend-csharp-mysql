using System.ComponentModel.DataAnnotations.Schema;

[Table("transactions")]
public class Transaction
{
    [Column("id")]
    public int Id { get; set; }

    [Column("transaction_id")]
    public required string TransactionId { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("note")]
    public string Note { get; set; } = string.Empty;

    [Column("amount")]
    public decimal Amount { get; set; }

    [Column("type")]
    public string Type { get; set; } = string.Empty;

    [Column("transaction_date")]
    public DateTime TransactionDate { get; set; }

    [Column("user_id")]
    public string UserId { get; set; } = string.Empty;

    [Column("wallet_id")]
    public string WalletId { get; set; } = string.Empty;

    [Column("category_id")]
    public string CategoryId { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime CreateAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}
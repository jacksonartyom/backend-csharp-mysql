using System.ComponentModel.DataAnnotations.Schema;

[Table("transactions")]
public class Transaction
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Title { get; set; } = string.Empty;

    [Column("amount")]
    public decimal Amount { get; set; }

    [Column("type")]
    public string Type { get; set; } = string.Empty;

    [Column("transaction_date")]
    public DateTime TransactionDate { get; set; }
}
using System.ComponentModel.DataAnnotations.Schema;

[Table("categories")]
public class Category
{
    [Column("id")]
    public int Id { get; set; }

    [Column("category_id")]
    public string CategoryId { get; set; } = string.Empty;

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("type")]
    public string Type { get; set; } = string.Empty;

    [Column("user_id")]
    public string? UserId { get; set; }

    [Column("is_system_default")]
    public bool IsSystemDefault { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

}
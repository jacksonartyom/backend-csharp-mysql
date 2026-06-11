using System.ComponentModel.DataAnnotations.Schema;

[Table("users")]
public class User
{
    [Column("id")]
    public int Id { get; set; }
    [Column("user_id")]
    public required string UserId { get; set; }
    [Column("email")]
    public string Email { get; set; } = string.Empty;
    [Column("first_name")]
    public string FirstName { get; set; } = string.Empty;
    [Column("mid_name")]
    public string MidName { get; set; } = string.Empty;
    [Column("last_name")]
    public string LastName { get; set; } = string.Empty;
    [Column("password")]
    public string Password { get; set; } = string.Empty;
    [Column("phone_no")]
    public string PhoneNo { get; set; } = string.Empty;
    [Column("image_profile")]
    public string? ImageProfile { get; set; }
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

}
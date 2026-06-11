public class CreateUserDto
{
    public int Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string MidName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string PhoneNo { get; set; } = string.Empty;

    public string? ImageProfile { get; set; }

}
public class UserSignInDto
{
    public string Email { get; set; } = string.Empty;
    public bool FlagSignIn { get; set; }
    public string? Token { get; set; }
}
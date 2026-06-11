public class UserSignInDto
{
    public string Email { get; set; } = String.Empty;
    public bool FlagSignIn { get; set; }
    public string? Token { get; set; }
}
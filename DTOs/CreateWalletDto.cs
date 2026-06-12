public class CreateWalletDto
{
    public string WalletName { get; set; } = string.Empty;
    public string WalletDetail { get; set; } = string.Empty;
    public decimal Balance { get; set; }
}
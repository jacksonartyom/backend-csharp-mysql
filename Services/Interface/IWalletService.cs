public interface IWalletService
{
    Task<List<WalletResponse>> GetWalletByUserId(string userId);
    Task<WalletResponse> CreateWallet(CreateWalletDto dto, string userId);
    Task<WalletResponse> UpdateWallet(CreateWalletDto dto, string walletId);
    Task<bool> DeleteWallet(string walletId);

}
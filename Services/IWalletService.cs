public interface IWalletService
{
    Task<List<Wallet>> GetWalletByUserId(string userId);
    Task<Wallet> CreateWallet(CreateWalletDto dto, string userId);
    Task<Wallet> UpdateWallet(CreateWalletDto dto, string walletId);
    Task<bool> DeleteWallet(string walletId);

}
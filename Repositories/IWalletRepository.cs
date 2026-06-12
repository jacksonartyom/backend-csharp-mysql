public interface IWalletRepository
{
        Task<List<Wallet>> GetWalletByUserId(string userId);

        Task<Wallet> Create(Wallet wallet);

        Task<Wallet?> GetWalletByWalletId(string walletId);

        Task<Wallet> Update(Wallet wallet);
        Task<bool> Delete(string walletId);
}
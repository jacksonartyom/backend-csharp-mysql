using System.Globalization;
using Microsoft.VisualBasic;

public class DashboardService : IDashboardService
{

    private readonly IWalletRepository _walletRepo;

    private readonly ITransactionRepository _transactionRepo;


    public DashboardService(IWalletRepository walletRepo, ITransactionRepository transactionRepo)
    {
        _walletRepo = walletRepo;
        _transactionRepo = transactionRepo;
    }

    public async Task<DashboardResponse> GetDashboard(
    string userId)
    {

        var wallets = await _walletRepo.GetWalletByUserId(userId);
        var walletsList = new List<WalletResponse>();
        foreach (var item in wallets)
        {
            var wallet = new WalletResponse
            {
                WalletId = item.WalletId,
                WalletName = item.WalletName,
                WalletDetail = item.WalletDetail,
                Balance = item.Balance,
                UserId = item.UserId
            };
            walletsList.Add(wallet);
        }
        ;

        var results = new DashboardResponse
        {
            TotalBalance = await _walletRepo.SumBalanceByUserId(userId),
            Wallets = walletsList,
            RecentTransactions = await _transactionRepo.FindByUserId(userId)
        };

        return results;

    }

}
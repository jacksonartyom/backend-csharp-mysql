public interface ITransactionRepository
{
    Task<List<TransactionDashboardResponse>> FindByWalletId(string walletId, DateTime dateFrom, DateTime dateTo);
    Task<Transaction?> GetTransactionByTransactionId(string transactionId);
    Task<List<Transaction>> CreateRange(List<Transaction> transaction);
    Task<Transaction> Update(Transaction transaction);
}
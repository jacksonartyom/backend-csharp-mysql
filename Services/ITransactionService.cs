public interface ITransactionService
{
    Task<List<TransactionDashboardResponse>> GetAll(string walletId, string month, string year);
    Task<List<Transaction>> Create(List<CreateTransactionDto> dto, string userId);
}
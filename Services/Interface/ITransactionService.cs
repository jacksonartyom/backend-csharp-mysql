public interface ITransactionService
{
    Task<List<TransactionResponse>> GetByMonthYear(string walletId, string month, string year);
    Task<List<Transaction>> Create(List<CreateTransactionDto> dto, string? userId);
}
public interface ITransactionService
{
    Task<List<Transaction>> GetAll();
    Task<Transaction> Create(CreateTransactionDto dto);
}
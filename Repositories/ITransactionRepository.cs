public interface ITransactionRepository
{
    Task<List<Transaction>> GetAll();
    Task<Transaction> Create(Transaction transaction);
}
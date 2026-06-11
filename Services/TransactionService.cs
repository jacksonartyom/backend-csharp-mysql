public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repo;

    public TransactionService(ITransactionRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Transaction>> GetAll()
    {
        return await _repo.GetAll();
    }

    public async Task<Transaction> Create(CreateTransactionDto dto)
    {
        var transaction = new Transaction
        {
            Title = dto.name,
            Amount = dto.amount,
            Type = dto.type,
            TransactionDate = dto.transactionDate
        };

        return await _repo.Create(transaction);
    }
}
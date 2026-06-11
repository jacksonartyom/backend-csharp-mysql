using Microsoft.EntityFrameworkCore;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Transaction>> GetAll()
    {
        return await _context.Transactions
        .Where(t => t.Amount > 100)
        .ToListAsync();
    }

    public async Task<Transaction> Create(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }
}
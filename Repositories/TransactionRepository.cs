using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    private readonly ILogger<WalletController> _logger;

    public TransactionRepository(AppDbContext context, ILogger<WalletController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<TransactionDetailResponse>> FindByWalletId(
        string walletId, DateTime dateFrom, DateTime dateTo)
    {
        var data = await _context.Transactions
            .Where(tran => tran.WalletId == walletId
                && tran.TransactionDate >= dateFrom
                && tran.TransactionDate < dateTo)
            .Join(_context.Category,
                tran => tran.CategoryId,
                cate => cate.CategoryId,
                (tran, cate) => new { tran, cate })
            .OrderByDescending(x => x.tran.TransactionDate)
            .ToListAsync(); // ดึงมาก่อน

        var result = data.Select(x => new TransactionDetailResponse
        {
            TransactionId = x.tran.TransactionId,
            WalletId = x.tran.WalletId,
            Name = x.tran.Name,
            Amount = x.tran.Amount,
            Note = x.tran.Note,
            Type = x.tran.Type,
            TransactionDate = x.tran.TransactionDate.ToString("yyyy-MM-dd"),
            CategoryId = x.tran.CategoryId,
            CategoryName = x.cate.Name,
            UserId = x.tran.UserId
        }).ToList();

        return result;
    }

    public async Task<Transaction?> GetTransactionByTransactionId(string transactionId)
    {
        return await _context.Transactions
                .FirstOrDefaultAsync(t => t.TransactionId == transactionId);
    }

    public async Task<List<Transaction>> CreateRange(List<Transaction> transactions)
    {
        await _context.Transactions.AddRangeAsync(transactions);
        await _context.SaveChangesAsync();
        return transactions;
    }

    public async Task<Transaction> Update(Transaction transaction)
    {
        _context.Transactions.Update(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }

    public async Task<List<TransactionDetailResponse>> FindByUserId(string userId)
    {
        var query = await (
            from tran in _context.Transactions
            join cate in _context.Category
                on tran.CategoryId equals cate.CategoryId into cateGroup
            from cate in cateGroup.DefaultIfEmpty()
            where tran.UserId == userId
            orderby tran.TransactionDate descending
            select new TransactionDetailResponse
            {
                TransactionId = tran.TransactionId,
                WalletId = tran.WalletId,
                Name = tran.Name,
                Amount = tran.Amount,
                Note = tran.Note,
                Type = tran.Type,
                TransactionDate = tran.TransactionDate.ToString("yyyy-MM-dd"),
                CategoryId = tran.CategoryId,
                CategoryName = cate != null ? cate.Name : null,
                UserId = tran.UserId
            }
        )
        .Take(5)
        .ToListAsync();

        return query;
    }
}
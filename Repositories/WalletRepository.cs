using Microsoft.EntityFrameworkCore;

public class WalletRepository : IWalletRepository
{
    private readonly AppDbContext _context;

    public WalletRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Wallet>> GetWalletByUserId(string userId)
    {
        return await _context.Wallet.Where(w => w.UserId == userId).ToListAsync();
    }


    public async Task<Wallet> Create(Wallet wallet)
    {
        _context.Wallet.Add(wallet);
        await _context.SaveChangesAsync();
        return wallet;
    }

    public async Task<Wallet?> GetWalletByWalletId(string walletId)
    {
        return await _context.Wallet.FirstOrDefaultAsync(w => w.WalletId == walletId);
    }

    public async Task<Wallet> Update(Wallet wallet)
    {
        _context.Wallet.Update(wallet);
        await _context.SaveChangesAsync();
        return wallet;
    }

    public async Task<bool> Delete(string walletId)
    {
        var wallet = await _context.Wallet
            .FirstOrDefaultAsync(w => w.WalletId == walletId);

        if (wallet == null)
            return false;

        _context.Wallet.Remove(wallet);

        var result = await _context.SaveChangesAsync();

        return result > 0;
    }


    public async Task UpdateBalance(string walletId, decimal amount, DateTime updatedAt)
    {
        var wallet = await _context.Wallet.FirstOrDefaultAsync(w => w.WalletId == walletId);

        if (wallet == null)
            throw new Exception("Wallet not found");

        wallet.Balance += amount;
        wallet.UpdatedAt = updatedAt;

        await _context.SaveChangesAsync();
    }
}
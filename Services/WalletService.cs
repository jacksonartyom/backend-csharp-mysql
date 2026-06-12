using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;

public class WalletService : IWalletService
{
    private readonly IWalletRepository _repo;

    private readonly ILogger<WalletController> _logger;

    public WalletService(IWalletRepository repo, ILogger<WalletController> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    public Task<List<Wallet>> GetWalletByUserId(string userId)
    {
        _logger.LogInformation("User in service {userId}", userId);
        return _repo.GetWalletByUserId(userId);
    }

    public async Task<Wallet> CreateWallet(CreateWalletDto dto, string userId)
    {

        var thaiTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        var wallet = new Wallet
        {
            WalletId = Guid.NewGuid().ToString("N"),
            WalletName = dto.WalletName,
            WalletDetail = dto.WalletDetail,
            Balance = dto.Balance,
            UserId = userId,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, thaiTimeZone)
        };

        return await _repo.Create(wallet);
    }

    public async Task<Wallet> UpdateWallet(CreateWalletDto dto, string walletId)
    {

        var wallet = await _repo.GetWalletByWalletId(walletId);
        if (wallet == null)
        {
            throw new Exception("Data not found");
        }
        var thaiTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        wallet.WalletName = dto.WalletName;
        wallet.WalletDetail = dto.WalletDetail;
        wallet.UpdatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, thaiTimeZone);


        return await _repo.Update(wallet);
    }

    public async Task<bool> DeleteWallet(string walletId)
    {
        var wallet = await _repo.GetWalletByWalletId(walletId);
        if (wallet == null)
        {
            throw new Exception("Data not found");
        }

        return await _repo.Delete(walletId);
    }
}
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

    public async Task<List<WalletResponse>> GetWalletByUserId(string userId)
    {
        var result = await _repo.GetWalletByUserId(userId);
        var responseList = new List<WalletResponse>();
        foreach (var item in result)
        {
            var response = new WalletResponse
            {
                WalletId = item.WalletId,
                WalletName = item.WalletName,
                WalletDetail = item.WalletDetail,
                Balance = item.Balance,
                UserId = item.UserId
            };
            responseList.Add(response);
        }
        return responseList;
    }

    public async Task<WalletResponse> CreateWallet(CreateWalletDto dto, string userId)
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

        await _repo.Create(wallet);

        var response = new WalletResponse
        {
            WalletId = wallet.WalletId,
            WalletName = wallet.WalletName,
            WalletDetail = wallet.WalletDetail,
            Balance = wallet.Balance,
            UserId = wallet.UserId
        };

        return response;
    }

    public async Task<WalletResponse> UpdateWallet(CreateWalletDto dto, string walletId)
    {

        var wallet = await _repo.GetWalletByWalletId(walletId);
        if (wallet == null)
        {
            throw new BadRequestException("Data not found");
        }
        var thaiTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

        wallet.WalletName = dto.WalletName;
        wallet.WalletDetail = dto.WalletDetail;
        wallet.UpdatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, thaiTimeZone);
        await _repo.Update(wallet);

        var response = new WalletResponse
        {
            WalletId = wallet.WalletId,
            WalletName = wallet.WalletName,
            WalletDetail = wallet.WalletDetail,
            Balance = wallet.Balance,
            UserId = wallet.UserId
        };

        return response;
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
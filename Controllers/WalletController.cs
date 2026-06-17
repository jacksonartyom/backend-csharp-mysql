using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

[ApiController]
[Route("api/v1/wallet")]
public class WalletController : ControllerBase
{
    private readonly IWalletService _service;
    private readonly ILogger<WalletController> _logger;

    public WalletController(IWalletService service, ILogger<WalletController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetWalletByUserId()
    {
        var userId = User.FindFirst("userId")?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("UserId not found in token");
        }

        var wallet = await _service.GetWalletByUserId(userId);
        var response = new ResponseDto<List<Wallet>>
        {
            Message = "Get wallet: Success",
            Result = wallet
        };
        return Ok(response);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateWallet(CreateWalletDto dto)
    {
        var userId = User.FindFirst("userId")?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("UserId not found in token");
        }

        var wallet = await _service.CreateWallet(dto, userId);
        var response = new ResponseDto<Wallet>
        {
            Message = "Create wallet: Success",
            Result = wallet
        };
        return Ok(response);
    }

    [Authorize]
    [HttpPut("{walletId}")]
    public async Task<IActionResult> UpdateWallet(
    [FromRoute] string walletId,
    [FromBody] CreateWalletDto dto)
    {
        var wallet = await _service.UpdateWallet(dto, walletId);
        var response = new ResponseDto<Wallet>
        {
            Message = "Update wallet: Success",
            Result = wallet
        };
        return Ok(response);
    }

    [Authorize]
    [HttpDelete("{walletId}")]
    public async Task<IActionResult> DeleteWallet(string walletId)
    {
        var wallet = await _service.DeleteWallet(walletId);
        var response = new ResponseDto<bool>
        {
            Message = "Delete wallet: Success",
            Result = wallet
        };
        return Ok(response);
    }
}
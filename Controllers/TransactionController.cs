using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/transaction")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _service;

    public TransactionController(ITransactionService service)
    {
        _service = service;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery(Name = "wallet_id")] string walletId,
    [FromQuery] string month,
    [FromQuery] string year)
    {
        var data = await _service.GetAll(walletId, month, year);
        return Ok(data);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(List<CreateTransactionDto> dto)
    {
        var userId = User.FindFirst("userId")?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("UserId not found in token");
        }
        var result = await _service.Create(dto, userId);
        return Ok(result);
    }
}
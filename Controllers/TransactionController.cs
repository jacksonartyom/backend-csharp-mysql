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
        var data = await _service.GetByMonthYear(walletId, month, year);
        var response = new ResponseDto<List<TransactionResponse>>
        {
            Message = "success",
            Result = data
        };
        return Ok(response);
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
        var response = new ResponseDto<List<Transaction>>
        {
            Message = "success",
            Result = result
        };
        return Ok(response);
    }
}
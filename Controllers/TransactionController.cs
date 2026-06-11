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

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var data = await _service.GetAll();
        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTransactionDto dto)
    {
        var result = await _service.Create(dto);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("test-db")]
    public async Task<IActionResult> TestDb()
    {
        var data = await _service.GetAll(); // ✅ ใช้ service
        return Ok(data);
    }
}
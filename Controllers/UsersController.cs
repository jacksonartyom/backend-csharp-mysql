using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _service;

    public UsersController(IUsersService service)
    {
        _service = service;
    }

    [HttpGet("user")]
    public IActionResult GetAll()
    {
        return Ok(new[] {
            new { id = 1, name = "John" }
        });
    }

    [HttpGet("user/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return BadRequest("Email is required");
        }

        var user = await _service.GetByEmail(email);

        return Ok(user);
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> CreateUserProfile(CreateUserDto dto)
    {
        var result = await _service.Create(dto);
        return Ok(result);
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignInUserProfile(SignInDto dto)
    {
        var result = await _service.SignIn(dto);
        return Ok(result);
    }
}
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

    [HttpGet("user/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return BadRequest("Email is required");
        }

        var user = await _service.GetByEmail(email);
        var response = new ResponseDto<User>
        {
            Message = "Get user by email: Success",
            Result = user
        };
        return Ok(response);
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> CreateUserProfile(CreateUserDto dto)
    {
        var result = await _service.Create(dto);

        var response = new ResponseDto<User>
        {
            Message = "Create User Profile: Success",
            Result = result
        };
        return Ok(response);
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignInUserProfile(SignInDto dto)
    {
        var result = await _service.SignIn(dto);

        var response = new ResponseDto<UserSignInDto>
        {
            Message = "Sign In: Success",
            Result = result
        };
        return Ok(response);
    }
}
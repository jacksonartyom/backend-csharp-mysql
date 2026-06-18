using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/v1")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _service;

    public UsersController(IUsersService service)
    {
        _service = service;
    }

    [Authorize]
    [HttpGet("user/user-profile")]
    public async Task<IActionResult> GetByUserId()
    {
        var userId = User.FindFirst("userId")?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized("UserId not found in token");
        }

        var user = await _service.GetByUserId(userId);
        var response = new ResponseDto<UserResponse>
        {
            Message = "success",
            Result = user
        };
        return Ok(response);
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> CreateUserProfile(CreateUserDto dto)
    {
        var result = await _service.Create(dto);

        var response = new ResponseDto<UserResponse>
        {
            Message = "success",
            Result = result
        };
        return Ok(response);
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignInUserProfile(SignInDto dto)
    {
        var result = await _service.SignIn(dto);

        var response = new ResponseDto<UserResponse>
        {
            Message = "success",
            Result = result
        };
        return Ok(response);


    }
}
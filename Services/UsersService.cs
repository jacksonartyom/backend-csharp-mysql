using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _repo;

    private readonly IConfiguration _config;

    public UsersService(IUsersRepository repo, IConfiguration config)
    {
        _repo = repo;
        _config = config;
    }

    public async Task<UserResponse> Create(CreateUserDto dto)
    {
        var checkUser = await _repo.GetUserByEmail(dto.Email);

        if (checkUser != null)
        {
            throw new BadRequestException("Email already had");
        }


        var hasher = new PasswordHasher<User>();

        var user = new User
        {
            UserId = Guid.NewGuid().ToString("N"),
            Email = dto.Email,
            FirstName = dto.FirstName,
            MidName = dto.MidName,
            LastName = dto.LastName,
            PhoneNo = dto.PhoneNo,
            ImageProfile = dto.ImageProfile,
            CreatedAt = DateTime.UtcNow
        };

        user.Password = hasher.HashPassword(user, dto.Password);

        await _repo.Create(user);

        var response = new UserResponse
        {
            UserId = user.UserId,
            Email = user.Email,
            FirstName = user.FirstName,
            MidName = user.MidName,
            LastName = user.LastName,
            PhoneNo = user.PhoneNo,
            ImageProfile = user.ImageProfile
        };

        return response;
    }

    public async Task<UserResponse> SignIn(SignInDto dto)
    {
        var user = await _repo.GetUserByEmail(dto.Email);

        if (user == null)
        {
            throw new BadRequestException("Not found user");
        }

        var hasher = new PasswordHasher<User>();

        var result = hasher.VerifyHashedPassword(
            user,
            user.Password,
            dto.Password
        );

        var isSuccess = result == PasswordVerificationResult.Success
                     || result == PasswordVerificationResult.SuccessRehashNeeded;

        if (!isSuccess)
        {
            throw new BadRequestException("Wrong password");
        }

        var token = GenerateJwtToken(user);

        var response = new UserResponse
        {
            UserId = user.UserId,
            Email = user.Email,
            FirstName = user.FirstName,
            MidName = user.MidName,
            LastName = user.LastName,
            PhoneNo = user.PhoneNo,
            Token = token,
            ImageProfile = user.ImageProfile
        };

        return response;
    }

    public string GenerateJwtToken(User user)
    {
        var keyString = _config["Jwt:Key"];

        if (string.IsNullOrEmpty(keyString))
        {
            throw new Exception("JWT Key is not configured");
        }

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(keyString)
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserId),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim("name", user.FirstName ?? ""),
        new Claim("userId", user.UserId ?? "")
    };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<UserResponse> GetByUserId(string userId)
    {
        var user = await _repo.GetUserByUserId(userId);

        if (user == null)
        {
            throw new Exception("Data not found");
        }

        var response = new UserResponse
        {
            UserId = user.UserId,
            Email = user.Email,
            FirstName = user.FirstName,
            MidName = user.MidName,
            LastName = user.LastName,
            PhoneNo = user.PhoneNo,
            ImageProfile = user.ImageProfile
        };

        return response;
    }
}
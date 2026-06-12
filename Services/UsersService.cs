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

    public async Task<List<User>> GetAll()
    {
        return await _repo.GetAll();
    }

    public async Task<User> Create(CreateUserDto dto)
    {
        var hasher = new PasswordHasher<User>();

        var user = new User
        {
            UserId = Guid.NewGuid().ToString("N"),
            Email = dto.Email,
            FirstName = dto.FirstName,
            MidName = dto.MidName,
            LastName = dto.LastName,
            Password = hasher.HashPassword(null, dto.Password),
            PhoneNo = dto.PhoneNo,
            ImageProfile = dto.ImageProfile,
            CreatedAt = DateTime.UtcNow
        };

        return await _repo.Create(user);
    }

    public async Task<UserSignInDto> SignIn(SignInDto dto)
    {
        var user = await _repo.GetUserByEmail(dto.Email);

        if (user == null)
        {
            return new UserSignInDto
            {
                Email = dto.Email,
                FlagSignIn = false
            };
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
            return new UserSignInDto
            {
                Email = dto.Email,
                FlagSignIn = false
            };
        }

        var token = GenerateJwtToken(user);

        return new UserSignInDto
        {
            Email = user.Email,
            FlagSignIn = true,
            Token = token
        };
    }

    public string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"])
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

    public async Task<User> GetByEmail(string email)
    {
        var user = await _repo.GetUserByEmail(email);

        if (user == null)
        {
            throw new Exception("Data not found");
        }

        return user;
    }
}
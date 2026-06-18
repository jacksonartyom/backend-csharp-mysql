public interface IUsersService
{
    Task<UserResponse> GetByUserId(string userId);
    Task<UserResponse> Create(CreateUserDto dto);
    Task<UserResponse> SignIn(SignInDto dto);
}
public interface IUsersService
{
    Task<List<User>> GetAll();
    Task<User> GetByEmail(string email);
    Task<User> Create(CreateUserDto dto);
    Task<UserSignInDto> SignIn(SignInDto dto);
}
public interface IUsersRepository
{
    Task<List<User>> GetAll();
    Task<User> Create(User user);

    Task<User?> GetUserByEmail(string email);

    Task<User?> GetUserByUserId(string userId);
}
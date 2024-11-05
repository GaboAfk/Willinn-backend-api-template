namespace Core.Services;

public interface IUserService
{
    //Task<User> Login(string email, string password);
    Task<User> AddUser(string name, string email, string password);
    Task<List<User>> GetAllUsers();
    Task<User> GetUserById(int id);
    Task<User> GetUserByEmail(string email);
    Task<User> UpdateUser(User user);
    Task<User> DeleteUser(int id);
}
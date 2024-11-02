using Core;
using Core.Services;
using Data;

namespace Services
{
    public class UserService(AppDbContext context) : IUserService
    {
        /*public Task<User> Login(string email, string password)
        {
            throw new NotImplementedException();
        }*/

        public async Task<User> AddUser(string name, string email, string password)
        {
            var newUser = new User
            {
                Name = name,
                Email = email,
                Password = password,
            };
            
            await context.SaveUserDB(newUser);
            
            return newUser;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await context.GetUsersDB();
        }

        public async Task<User> GetUserById(int id)
        {
            return await context.GetUserDB(id);
        }

        public async Task<User> UpdateUser(User user)
        {
            await context.UpdateUserDB(user);
            var updatedUser = await GetUserById(user.ID);
            return updatedUser;
        }

        public async Task<User> DeleteUser(int id)
        {
            var userToDelete = await context.GetUserDB(id);
            userToDelete.IsActive = false;

            await context.UpdateUserDB(userToDelete);
            var deletedUser = await GetUserById(userToDelete.ID);
            return deletedUser;
        }
    }
}

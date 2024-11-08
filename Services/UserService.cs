using Core;
using Core.Services;
using Data;

namespace Services
{
    public class UserService(AppDbContext context, IJwtService jwtService) : IUserService
    {
        public async Task<User> AddUser(string name, string email, string password, bool isActive)
        {
            var newUser = new User
            {
                Name = name,
                Email = email,
                Password = jwtService.EncrypterSha256(password),
                IsActive = isActive
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
            return await context.GetUserDBById(id);
        }
        
        public async Task<User> GetUserByEmail(string email)
        {
            return await context.GetUserDBByEmail(email);
        }

        public async Task<User> UpdateUser(User user)
        {
            var updatedUser  = await GetUserById(user.ID);
            if (updatedUser  == null)
            {
                throw new KeyNotFoundException($"User  with ID {user.ID} not found.");
            }

            updatedUser.Name = user.Name;
            updatedUser.Email = user.Email;
            updatedUser.Password = jwtService.EncrypterSha256(user.Password);

            await context.UpdateUserDB(updatedUser);
            return updatedUser;
        }

        public async Task<User> DeleteUser(int id)
        {
            var userToDelete = await context.GetUserDBById(id);
            userToDelete.IsActive = false;

            await context.UpdateUserDB(userToDelete);
            var deletedUser = await GetUserById(userToDelete.ID);
            return deletedUser;
        }
    }
}

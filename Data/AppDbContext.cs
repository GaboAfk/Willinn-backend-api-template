using Core;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        public async Task SaveUserDB(User user)
        {
            Users.Add(user);
            await SaveChangesAsync();
        }

        public async Task<List<User>> GetUsersDB()
        {
            var usersList = await Users.ToListAsync();
            return usersList;
        }

        public async Task<User> GetUserDB(int id)
        {
            var user = await Users.FindAsync(id);
            return user;
        }

        public async Task UpdateUserDB(User user)
        {
            Users.Update(user);
            await SaveChangesAsync();
        }
    }
}

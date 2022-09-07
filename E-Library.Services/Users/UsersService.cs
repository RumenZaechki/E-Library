using E_Library.Data;
using E_Library.Services.Contracts;
using E_Library.Services.Users.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Library.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly LibraryDbContext data;
        public UsersService(LibraryDbContext data)
        {
            this.data = data;
        }

        public async Task<IEnumerable<UserServiceModel>> GetAllUsersAsync(string adminId)
        {
            return await this.data.Users
                .Where(u => u.Id != adminId)
                .Select(u => new UserServiceModel
                {
                    Id = u.Id,
                    Username = u.UserName
                })
                .ToListAsync();
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await this.data.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                this.data.Users.Remove(user);
                await this.data.SaveChangesAsync();
            }
        }
    }
}

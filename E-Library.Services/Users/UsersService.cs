using E_Library.Data;
using E_Library.Services.Contracts;
using E_Library.Services.Users.Models;

namespace E_Library.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly LibraryDbContext data;
        public UsersService(LibraryDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<UserServiceModel> GetAllUsers(string adminId)
        {
            return this.data.Users
                .Where(u => u.Id != adminId)
                .Select(u => new UserServiceModel
                {
                    Id = u.Id,
                    Username = u.UserName
                })
                .ToList();
        }

        public void DeleteUser(string userId)
        {
            var user = this.data.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return;
            }
            this.data.Users.Remove(user);
            this.data.SaveChanges();
        }
    }
}

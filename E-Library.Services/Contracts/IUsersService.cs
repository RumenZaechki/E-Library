using E_Library.Services.Users.Models;

namespace E_Library.Services.Contracts
{
    public interface IUsersService
    {
        public Task<IEnumerable<UserServiceModel>> GetAllUsersAsync(string adminId);
        public Task DeleteUserAsync(string userId);
    }
}

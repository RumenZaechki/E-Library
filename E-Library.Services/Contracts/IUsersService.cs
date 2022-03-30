using E_Library.Services.Users.Models;

namespace E_Library.Services.Contracts
{
    public interface IUsersService
    {
        public IEnumerable<UserServiceModel> GetAllUsers(string adminId);
        public void DeleteUser(string userId);
    }
}

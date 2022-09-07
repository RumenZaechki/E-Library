using E_Library.Data.Models;
using E_Library.Services.Users;
using E_Library.Test.Mocks;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace E_Library.Test.Services
{
    public class UsersServiceTests
    {
        [Fact]
        public async Task GetAllUsersShouldReturnCorrectResultWhenThereAreUsersInTheDb()
        {
            var data = DbMock.Instance;
            var user = GetUser();
            data.Users.Add(user);
            data.SaveChanges();
            var usersService = new UsersService(data);

            var result = await usersService.GetAllUsersAsync("");
            var res = result.ToList();

            Assert.NotEmpty(result);
            Assert.Equal(res[0].Username, user.UserName);
        }

        [Fact]
        public async Task GetAllUsersShouldReturnEmptyCollectionWhenNoUsersAreInTheDb()
        {
            var data = DbMock.Instance;
            var usersService = new UsersService(data);

            var result = await usersService.GetAllUsersAsync("");

            Assert.Empty(result);
        }

        [Fact]
        public async Task DeleteUserWorksCorrectlyWhenGivenCorrectInput()
        {
            var data = DbMock.Instance;
            var user = GetUser();
            data.Users.Add(user);
            data.SaveChanges();

            Assert.Single(data.Users);

            var usersService = new UsersService(data);
            await usersService.DeleteUserAsync(user.Id);

            Assert.Empty(data.Users);
        }

        [Fact]
        public async Task DeleteUserDoesNothingWhenGivenIncorrectInput()
        {
            var data = DbMock.Instance;
            var user = GetUser();
            data.Users.Add(user);
            data.SaveChanges();
            var usersService = new UsersService(data);

            await usersService.DeleteUserAsync("invalidUserId");

            Assert.NotEmpty(data.Users);
        }

        private User GetUser()
        {
            return new User
            {
                UserName = "RandomDude",
                Cart = new Cart()
            };
        }
    }
}

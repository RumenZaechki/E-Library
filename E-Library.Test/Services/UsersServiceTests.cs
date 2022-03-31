using E_Library.Data.Models;
using E_Library.Services.Users;
using E_Library.Test.Mocks;
using System.Linq;
using Xunit;

namespace E_Library.Test.Services
{
    public class UsersServiceTests
    {
        [Fact]
        public void GetAllUsersShouldReturnCorrectResultWhenThereAreUsersInTheDb()
        {
            var data = DbMock.Instance;
            var user = GetUser();
            data.Users.Add(user);
            data.SaveChanges();
            var usersService = new UsersService(data);

            var result = usersService.GetAllUsers("").ToList();

            Assert.NotEmpty(result);
            Assert.Equal(result[0].Username, user.UserName);
        }

        [Fact]
        public void GetAllUsersShouldReturnEmptyCollectionWhenNoUsersAreInTheDb()
        {
            var data = DbMock.Instance;
            var usersService = new UsersService(data);

            var result = usersService.GetAllUsers("");

            Assert.Empty(result);
        }

        [Fact]
        public void DeleteUserWorksCorrectlyWhenGivenCorrectInput()
        {
            var data = DbMock.Instance;
            var user = GetUser();
            data.Users.Add(user);
            data.SaveChanges();

            Assert.Single(data.Users);

            var usersService = new UsersService(data);
            usersService.DeleteUser(user.Id);

            Assert.Empty(data.Users);
        }

        [Fact]
        public void DeleteUserDoesNothingWhenGivenIncorrectInput()
        {
            var data = DbMock.Instance;
            var user = GetUser();
            data.Users.Add(user);
            data.SaveChanges();
            var usersService = new UsersService(data);

            usersService.DeleteUser("invalidUserId");

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

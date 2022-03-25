using E_Library.Data.Models;
using E_Library.Infrastructure;
using E_Library.Services.Contracts;
using E_Library.Services.Statistics;
using E_Library.Test.Mocks;
using System.Linq;
using Xunit;

namespace E_Library.Test.Services
{
    public class StatisticsServiceTests
    {
        [Fact]
        public void GetTotalUsersReturnsCorrectResultWhenDbIsNotEmpty()
        {
            var data = DbMock.Instance;
            var users = Enumerable
                .Range(0, 10)
                .Select(u => new User());
            data.Users.AddRange(users);
            data.SaveChanges();
            IStatisticsService statisticsService = new StatisticsService(data);

            var result = statisticsService.GetTotalUsers();

            Assert.Equal(10, result);
        }

        [Fact]
        public void GetTotalUsersReturnsZeroWhenDbIsEmpty()
        {
            var data = DbMock.Instance;
            IStatisticsService statisticsService = new StatisticsService(data);

            var result = statisticsService.GetTotalUsers();

            Assert.Equal(0, result);
        }

        [Fact]
        public void GetTotalBooksReturnsCorrectResultWhenDbIsNotEmpty()
        {
            var data = DbMock.Instance;
            BookSeeder.SeedBooks(data);
            
            IStatisticsService statisticsService = new StatisticsService(data);

            var result = statisticsService.GetTotalBooks();

            Assert.Equal(8, result);
        }

        [Fact]
        public void GetTotalBooksReturnsZeroWhenDbIsEmpty()
        {
            var data = DbMock.Instance;
            IStatisticsService statisticsService = new StatisticsService(data);

            var result = statisticsService.GetTotalBooks();

            Assert.Equal(0, result);
        }
    }
}

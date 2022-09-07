using E_Library.Data;
using E_Library.Data.Models;
using E_Library.Services.Contracts;
using E_Library.Services.Publishers;
using E_Library.Test.Mocks;
using System.Threading.Tasks;
using Xunit;

namespace E_Library.Test.Services
{
    public class PublishersServiceTests
    {
        [Fact]
        public async Task DetailsShouldReturnCorrectPublisherWhenDbIsNotEmpty()
        {
            var data = DbMock.Instance;
            AddPublishersToData(data);
            IPublishersService publishersService = new PublishersService(data);

            var actual = await publishersService.DetailsAsync("1");

            Assert.NotNull(actual);
            Assert.Equal("RandomDude", actual.Name);
        }

        [Fact]
        public async Task DetailsShouldReturnNullWhenDbIsEmpty()
        {
            var data = DbMock.Instance;
            IPublishersService publishersService = new PublishersService(data);

            var actual = await publishersService.DetailsAsync("1");

            Assert.Null(actual);
        }

        [Fact]
        public async Task DetailsShouldReturnNullWhenGivenWrongInput()
        {
            var data = DbMock.Instance;
            AddPublishersToData(data);
            IPublishersService publishersService = new PublishersService(data);

            var actual = await publishersService.DetailsAsync("125362342");

            Assert.Null(actual);
        }
        public async Task AddPublishersToData(LibraryDbContext data)
        {
            await data.Publishers.AddRangeAsync(new[]
            {
                new Publisher
                {
                    Id = "1",
                    Name = "RandomDude"
                },
                new Publisher
                {
                    Id = "2",
                    Name = "AnotherRandomDude"
                }
            });
            await data.SaveChangesAsync();
        }
    }
}

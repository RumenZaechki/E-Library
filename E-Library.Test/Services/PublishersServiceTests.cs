using E_Library.Data;
using E_Library.Data.Models;
using E_Library.Services.Contracts;
using E_Library.Services.Publishers;
using E_Library.Test.Mocks;
using Xunit;

namespace E_Library.Test.Services
{
    public class PublishersServiceTests
    {
        [Fact]
        public void DetailsShouldReturnCorrectPublisherWhenDbIsNotEmpty()
        {
            var data = DbMock.Instance;
            AddPublishersToData(data);
            IPublishersService publishersService = new PublishersService(data);

            var actual = publishersService.Details("1");

            Assert.NotNull(actual);
            Assert.Equal("RandomDude", actual.Name);
        }

        [Fact]
        public void DetailsShouldReturnNullWhenDbIsEmpty()
        {
            var data = DbMock.Instance;
            IPublishersService publishersService = new PublishersService(data);

            var actual = publishersService.Details("1");

            Assert.Null(actual.Name);
        }

        [Fact]
        public void DetailsShouldReturnNullWhenGivenWrongInput()
        {
            var data = DbMock.Instance;
            AddPublishersToData(data);
            IPublishersService publishersService = new PublishersService(data);

            var actual = publishersService.Details("125362342");

            Assert.Null(actual.Name);
        }
        public void AddPublishersToData(LibraryDbContext data)
        {
            data.Publishers.AddRange(new[]
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
            data.SaveChanges();
        }
    }
}

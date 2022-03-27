using E_Library.Data;
using E_Library.Data.Models;
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
            SeedBooks(data);
            
            IStatisticsService statisticsService = new StatisticsService(data);

            var result = statisticsService.GetTotalBooks();

            Assert.Equal(4, result);
        }

        [Fact]
        public void GetTotalBooksReturnsZeroWhenDbIsEmpty()
        {
            var data = DbMock.Instance;
            IStatisticsService statisticsService = new StatisticsService(data);

            var result = statisticsService.GetTotalBooks();

            Assert.Equal(0, result);
        }

        private void SeedBooks(LibraryDbContext data)
        {
            data.Books.AddRange(new[]
            {
                new Book
                {
                    Title ="Notes from the end of everything",
                    Description = "After being diagnosed with a brain tumor, writer John Gallo spends his time confronting his lifelong sense of fraudulence, regret, and self-misunderstanding, all while loosely chronicling the development of his cancer.",
                    Price = 10m,
                    ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/417Y0vAzvML._SY291_BO1,204,203,200_QL40_FMwebp_.jpg",
                    Release = 2020,
                    AuthorId = "1",
                    PublisherId = "1",
                    CategoryId = 3,
                },
                new Book
                {
                    Title ="Lord Of The Rings",
                    Description = "The Lord of the Rings is an epic high-fantasy novel by English author and scholar J. R. R. Tolkien. Set in Middle-earth, intended to be Earth at some distant time in the past, the story began as a sequel to Tolkien's 1937 children's book The Hobbit, but eventually developed into a much larger work.",
                    Price = 40m,
                    ImageUrl = "https://m.media-amazon.com/images/P/0618640150.01._SCLZZZZZZZ_SX500_.jpg",
                    Release = 1954,
                    AuthorId = "2",
                    PublisherId = "2",
                    CategoryId = 1,
                },
                new Book
                {
                    Title ="Meditations",
                    Description = "Meditations is a series of personal writings by Marcus Aurelius, Roman Emperor 161–180 CE, setting forth his ideas on Stoic philosophy.Marcus Aurelius wrote the 12 books of the Meditations in Koine Greek as a source for his own guidance and self-improvement.",
                    Price = 15m,
                    ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/51cQEdN9KuL._SX331_BO1,204,203,200_.jpg",
                    Release = 2018,
                    AuthorId = "3",
                    PublisherId = "3",
                    CategoryId = 8,
                },
                new Book
                {
                    Title ="The Art of War",
                    Description = "Sun Tzu is thought to have been a military general and adviser to the king of the Chinese state of Wu during the sixth century BCE. Although some modern scholars have called his authorship into doubt, the world's most influential treatise on military strategy, The Art of War, bears his name.",
                    Price = 11m,
                    ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/4145Q3WAneL._SX331_BO1,204,203,200_.jpg",
                    Release = 2000,
                    AuthorId = "4",
                    PublisherId = "4",
                    CategoryId = 7,
                }
            });
            data.SaveChanges();
        }
    }
}

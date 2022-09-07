using E_Library.Data.Models;
using E_Library.Services.Contracts;
using E_Library.Services.Reviews;
using E_Library.Services.Reviews.Models;
using E_Library.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace E_Library.Test.Services
{
    public class ReviewsServiceTests
    {
        [Fact]
        public async Task DeleteReviewWorksCorrectlyWhenGivenCorrectInput()
        {
            var data = DbMock.Instance;
            var user = GetUser();
            var book = GetBook();
            data.Users.Add(user);
            data.Books.Add(book);
            data.SaveChanges();
            int firstRating = 5;
            string firstDescription = "Good enough, I guess";
            int secondRating = 2;
            string secondDescription = "Actually, no, it's utter garbage.";
            IReviewsService reviewsService = new ReviewsService(data);
            await reviewsService.AddReviewAsync(book.Id, user.Id, firstRating, firstDescription);
            await reviewsService.AddReviewAsync(book.Id, user.Id, secondRating, secondDescription);
            var firstReviewId = data.Reviews
                .FirstOrDefault(r => r.UserId == user.Id && r.Description == firstDescription) 
                .Id;

            await reviewsService.DeleteReviewAsync(firstReviewId);
            var actual = book.Reviews.FirstOrDefault();

            Assert.Equal(1, book.Reviews.Count);
            Assert.Equal(1, user.Reviews.Count);
            Assert.Equal(secondRating, actual.Rating);
            Assert.Equal(secondDescription, actual.Description);
        }

        [Fact]
        public async Task DeleteReviewShouldDoNothingWhenGivenIncorrectInput()
        {
            var data = DbMock.Instance;
            var user = GetUser();
            var book = GetBook();
            data.Users.Add(user);
            data.Books.Add(book);
            data.SaveChanges();
            int firstRating = 5;
            string firstDescription = "Good enough, I guess";
            int secondRating = 2;
            string secondDescription = "Actually, no, it's utter garbage.";
            IReviewsService reviewsService = new ReviewsService(data);
            await reviewsService.AddReviewAsync(book.Id, user.Id, firstRating, firstDescription);
            await reviewsService.AddReviewAsync(book.Id, user.Id, secondRating, secondDescription);

            await reviewsService.DeleteReviewAsync("incorrectId");

            Assert.Equal(2, book.Reviews.Count);
            Assert.Equal(2, user.Reviews.Count);
        }

        [Fact]
        public async Task GetAllReviewsShouldReturnCorrectResultWhenGivenCorrectInput()
        {
            var data = DbMock.Instance;
            var user = GetUser();
            var book = GetBook();
            data.Users.Add(user);
            data.Books.Add(book);
            data.SaveChanges();
            int firstRating = 5;
            string firstDescription = "Good enough, I guess";
            int secondRating = 2;
            string secondDescription = "Actually, no, it's utter garbage.";
            IReviewsService reviewsService = new ReviewsService(data);
            await reviewsService.AddReviewAsync(book.Id, user.Id, firstRating, firstDescription);
            await reviewsService.AddReviewAsync(book.Id, user.Id, secondRating, secondDescription);

            var expected = new List<ReviewServiceModel>
            {
                new ReviewServiceModel
                {
                    Rating = secondRating,
                    Description= secondDescription
                },
                new ReviewServiceModel
                {
                    Rating = firstRating,
                    Description = firstDescription
                }
            };
            var actual = await reviewsService.GetAllReviewsAsync(book.Id);
            var res = actual.ToList();
            Assert.Equal(expected[0].Rating, res[0].Rating);
            Assert.Equal(expected[0].Description, res[0].Description);
            Assert.Equal(expected[1].Rating, res[1].Rating);
            Assert.Equal(expected[1].Description, res[1].Description);
        }

        [Fact]
        public async Task GetAllReviewsShouldReturnNullWhenGivenIncorrectInput()
        {
            var data = DbMock.Instance;
            var user = GetUser();
            var book = GetBook();
            data.Users.Add(user);
            data.Books.Add(book);
            data.SaveChanges();
            int firstRating = 5;
            string firstDescription = "Good enough, I guess";
            int secondRating = 2;
            string secondDescription = "Actually, no, it's utter garbage.";
            IReviewsService reviewsService = new ReviewsService(data);
            await reviewsService.AddReviewAsync(book.Id, user.Id, firstRating, firstDescription);
            await reviewsService.AddReviewAsync(book.Id, user.Id, secondRating, secondDescription);

            var result = await reviewsService.GetAllReviewsAsync("incorrectId");
            var res = result.ToList();
            Assert.Empty(res);
        }

        [Fact]
        public async Task AddReviewWorksCorrectlyWhenGivenCorrectInput()
        {
            var data = DbMock.Instance;
            var user = GetUser();
            var book = GetBook();
            data.Users.Add(user);
            data.Books.Add(book);
            data.SaveChanges();
            int rating = 5;
            string description = "Good enough, I guess";
            IReviewsService reviewsService = new ReviewsService(data);

            await reviewsService.AddReviewAsync(book.Id, user.Id, rating, description);

            Assert.Equal(1, book.Reviews.Count);
            Assert.Equal(1, user.Reviews.Count);
        }

        [Fact]
        public async Task AddReviewShouldDoNothingWhenGivenIncorrectInput()
        {
            var data = DbMock.Instance;
            var user = GetUser();
            var book = GetBook();
            data.Users.Add(user);
            data.Books.Add(book);
            data.SaveChanges();
            int rating = 5;
            string description = "Good enough, I guess";
            IReviewsService reviewsService = new ReviewsService(data);

            await reviewsService.AddReviewAsync("incorrectId", "anotherIncorrectId", rating, description);

            Assert.Equal(0, book.Reviews.Count);
            Assert.Equal(0, user.Reviews.Count);
        }

        private User GetUser()
        {
            return new User();
        }

        private Book GetBook()
        {
            return new Book
            {
                Title = "Notes from the end of everything",
                Description = "After being diagnosed with a brain tumor, writer John Gallo spends his time confronting his lifelong sense of fraudulence, regret, and self-misunderstanding, all while loosely chronicling the development of his cancer.",
                Price = 10m,
                ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/417Y0vAzvML._SY291_BO1,204,203,200_QL40_FMwebp_.jpg",
                Release = 2020,
                AuthorId = Guid.NewGuid().ToString(),
                Author = new Author
                {
                    Name = "Robert Pantano",
                    Description = "Robert Pantano is the creator of the YouTube channel and production house known as Pursuit of Wonder, which covers similar topics of philosophy, science, and literature through short stories, guided experiences, video essays, and more.",
                    ImageUrl = "https://yt3.ggpht.com/ytc/AKedOLSOgx4aSr0YiJreg-4ReQwO4hKw_wbVSKcrIf5JCQ=s900-c-k-c0x00ffffff-no-rj"
                },
                PublisherId = Guid.NewGuid().ToString(),
                Publisher = new Publisher
                {
                    Name = "Independently published"
                },
                CategoryId = 3,
            };
        }
    }
}

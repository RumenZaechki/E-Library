using E_Library.Data;
using E_Library.Data.Models;
using E_Library.Services.Contracts;
using E_Library.Services.Reviews.Models;

namespace E_Library.Services.Reviews
{
    public class ReviewsService : IReviewsService
    {
        private readonly LibraryDbContext data;
        public ReviewsService(LibraryDbContext data)
        {
            this.data = data;
        }
        public void DeleteReview(string reviewId)
        {
            Review review = this.data.Reviews.FirstOrDefault(r => r.Id == reviewId);
            if (review != null)
            {
                User user = this.data.Users.FirstOrDefault(u => u.Id == review.UserId);
                Book book = this.data.Books.FirstOrDefault(b => b.Id == review.BookId);
                user.Reviews.Remove(review);
                book.Reviews.Remove(review);
                this.data.Reviews.Remove(review);
                this.data.SaveChanges();
            }
        }
        public void AddReview(string bookId, string userId, int rating, string description)
        {
            Book book = this.data.Books.FirstOrDefault(b => b.Id == bookId);
            User user = this.data.Users.FirstOrDefault(u => u.Id == userId);
            if (book != null && user != null)
            {
                Review review = new Review
                {
                    BookId = bookId,
                    Book = book,
                    UserId = userId,
                    User = user,
                    Rating = rating,
                    Description = description
                };
                book.Reviews.Add(review);
                user.Reviews.Add(review);
                this.data.SaveChanges();
            }
        }
        public ICollection<ReviewServiceModel> GetAllReviews(string bookId)
        {
            return this.data.Reviews
                .Where(x => x.Book.Id == bookId)
                .OrderByDescending(r => r.CreatedOn)
                .Select(r => new ReviewServiceModel
                {
                    Id = r.Id,
                    Rating = r.Rating,
                    Description = r.Description,
                    User = r.User.UserName,
                })
                .ToList();
        }
    }
}

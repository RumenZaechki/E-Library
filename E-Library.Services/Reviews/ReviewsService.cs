using E_Library.Data;
using E_Library.Data.Models;
using E_Library.Services.Contracts;
using E_Library.Services.Reviews.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Library.Services.Reviews
{
    public class ReviewsService : IReviewsService
    {
        private readonly LibraryDbContext data;
        public ReviewsService(LibraryDbContext data)
        {
            this.data = data;
        }
        public async Task DeleteReviewAsync(string reviewId)
        {
            Review review = await this.data.Reviews.FirstOrDefaultAsync(r => r.Id == reviewId);
            if (review != null)
            {
                User user = await this.data.Users.FirstOrDefaultAsync(u => u.Id == review.UserId);
                Book book = await this.data.Books.FirstOrDefaultAsync(b => b.Id == review.BookId);
                user.Reviews.Remove(review);
                book.Reviews.Remove(review);
                this.data.Reviews.Remove(review);
                await this.data.SaveChangesAsync();
            }
        }
        public async Task AddReviewAsync(string bookId, string userId, int rating, string description)
        {
            Book book = await this.data.Books.FirstOrDefaultAsync(b => b.Id == bookId);
            User user = await this.data.Users.FirstOrDefaultAsync(u => u.Id == userId);
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
                await this.data.SaveChangesAsync();
            }
        }
        public async Task<ICollection<ReviewServiceModel>> GetAllReviewsAsync(string bookId)
        {
            return await this.data.Reviews
                .Where(x => x.Book.Id == bookId)
                .OrderByDescending(r => r.CreatedOn)
                .Select(r => new ReviewServiceModel
                {
                    Id = r.Id,
                    Rating = r.Rating,
                    Description = r.Description,
                    User = r.User.UserName,
                })
                .ToListAsync();
        }
    }
}

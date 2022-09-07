using E_Library.Services.Reviews.Models;

namespace E_Library.Services.Contracts
{
    public interface IReviewsService
    {
        public Task DeleteReviewAsync(string reviewId);
        public Task AddReviewAsync(string bookId, string userId, int rating, string description);
        public Task<ICollection<ReviewServiceModel>> GetAllReviewsAsync(string bookId);
    }
}

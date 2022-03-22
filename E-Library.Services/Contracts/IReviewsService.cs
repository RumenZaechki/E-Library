using E_Library.Services.Reviews.Models;

namespace E_Library.Services.Contracts
{
    public interface IReviewsService
    {
        public void DeleteReview(string reviewId);
        public void AddReview(string bookId, string userId, int rating, string description);
        public ICollection<ReviewServiceModel> GetAllReviews(string bookId);
    }
}

using E_Library.Areas.Identity;
using E_Library.Models.Reviews;
using E_Library.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static E_Library.WebConstants;


namespace E_Library.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewsService reviewsService;
        public ReviewsController(IReviewsService reviewsService)
        {
            this.reviewsService = reviewsService;
        }
        public async Task<IActionResult> All(string bookId)
        {
            var comments = await this.reviewsService
                .GetAllReviewsAsync(bookId);
            var res = comments
                .Select(r => new CommentsViewModel
                {
                    Id = r.Id,
                    Rating = r.Rating,
                    Description = r.Description,
                    User = r.User,
                })
                .ToList();
            var reviewModel = new ReviewViewModel
            {
                Comments = res,
                BookId = bookId
            };
            return View(reviewModel);
        }
        [Authorize(Roles = UserConstants.UserRoleName)]
        public IActionResult AddReview(string bookId)
        {
            return View(new AddReviewFormModel
            {
                BookId = bookId
            });
        }
        [Authorize(Roles = UserConstants.UserRoleName)]
        [HttpPost]
        public async Task<IActionResult> AddReview(AddReviewFormModel reviewModel)
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.reviewsService.AddReviewAsync(reviewModel.BookId, userId, reviewModel.Rating, reviewModel.Description);
            this.TempData[GlobalMessageKey] = "Successfully added review to the book.";
            return RedirectToAction("All", "Reviews", new { bookId = reviewModel.BookId });
        }
    }
}

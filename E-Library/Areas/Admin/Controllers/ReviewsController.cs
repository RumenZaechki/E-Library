using E_Library.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static E_Library.WebConstants;

namespace E_Library.Areas.Admin.Controllers
{
    public class ReviewsController : AdminController
    {
        private readonly IReviewsService reviewsService;
        public ReviewsController(IReviewsService reviewsService)
        {
            this.reviewsService = reviewsService;
        }

        [Authorize]
        public async Task<IActionResult> Delete(string commentId, string bookId)
        {
            await this.reviewsService.DeleteReviewAsync(commentId);
            this.TempData[GlobalMessageKey] = "Successfully removed review from book.";
            return RedirectToAction("All", "Reviews", new {area = "", bookId = bookId});
        }
    }
}

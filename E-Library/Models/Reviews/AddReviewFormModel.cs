using E_Library.Models.Constants;
using System.ComponentModel.DataAnnotations;

namespace E_Library.Models.Reviews
{
    public class AddReviewFormModel
    {
        [Required]
        public string BookId { get; set; }
        [Required]
        [Range(ReviewConstants.ReviewRatingMinValue, ReviewConstants.ReviewRatingMaxValue)]
        public int Rating { get; set; }
        [Required]
        [MaxLength(ReviewConstants.ReviewDescriptionMaxLength)]
        public string Description { get; set; }
    }
}

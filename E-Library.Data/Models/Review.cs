using E_Library.Data.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Library.Data.Models
{
    public class Review
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [MaxLength(ReviewConstants.ReviewDescriptionMaxLength)]
        public string Description { get; set; }
        [Required]
        [Range(ReviewConstants.ReviewRatingMinValue, ReviewConstants.ReviewRatingMaxValue)]
        public int Rating { get; set; }
        [Required]
        public string BookId { get; set; }
        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey(nameof (UserId))]
        public User User { get; set; }
    }
}

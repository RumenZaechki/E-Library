using E_Library.Models.Constants;
using System.ComponentModel.DataAnnotations;

namespace E_Library.Models.Books
{
    public class BookFormModel
    {
        [Required]
        [MaxLength(BookConstants.TitleMaxLength)]
        public string Title { get; set; }
        [Required]
        [MaxLength(BookConstants.DescriptionMaxLength)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Display(Name = "Image URL")]
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public int Release { get; set; }
        [Required]
        [MaxLength(AuthorConstants.AuthorNameMaxLength)]
        public string Author { get; set; }
        public string AuthorImage { get; set; }
        [Required]
        [MaxLength(AuthorConstants.AuthorDescriptionMaxLength)]
        public string AuthorDescription { get; set; }
        public string Publisher { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public IEnumerable<BookCategoryViewModel>? Categories { get; set; }
    }
}

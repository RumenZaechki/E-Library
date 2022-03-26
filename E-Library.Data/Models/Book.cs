using E_Library.Data.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Library.Data.Models
{
    public class Book
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [StringLength(BookConstants.TitleMaxLength)]
        public string Title { get; set; }
        [Required]
        [StringLength(BookConstants.DescriptionMaxLength)]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public int Release { get; set; }
        [Required]
        public string AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public Author Author { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        [Required]
        public string PublisherId { get; set; }
        [ForeignKey(nameof(PublisherId))]
        public Publisher Publisher { get; set; }
        public ICollection<BookCart> BookCarts { get; set; } = new List<BookCart>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
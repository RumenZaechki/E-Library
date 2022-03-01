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
        public DateTime Release { get; set; }
        [Required]
        public string AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public Author Author { get; set; }
        [Required]
        public string CartId { get; set; }

        [ForeignKey(nameof(CartId))]
        public Cart Cart { get; set; }
    }
}
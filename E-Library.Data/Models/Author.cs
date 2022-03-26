using E_Library.Data.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Library.Data.Models
{
    public class Author
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [StringLength(AuthorConstants.AuthorNameMaxLength)]
        public string Name { get; set; }
        [StringLength(AuthorConstants.AuthorDescriptionMaxLength)]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public string PublisherId { get; set; }
        [ForeignKey(nameof(PublisherId))]
        public Publisher Publisher { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
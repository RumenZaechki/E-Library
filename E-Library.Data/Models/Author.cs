using E_Library.Data.Constants;
using System.ComponentModel.DataAnnotations;

namespace E_Library.Data.Models
{
    public class Author
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        [StringLength(AuthorConstants.AuthorNameMaxLength)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
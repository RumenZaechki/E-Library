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
        public string FirstName { get; set; }
        [Required]
        [StringLength(AuthorConstants.AuthorNameMaxLength)]
        public string LastName { get; set; }
    }
}
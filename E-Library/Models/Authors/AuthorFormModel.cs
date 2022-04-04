using E_Library.Models.Constants;
using System.ComponentModel.DataAnnotations;

namespace E_Library.Models.Authors
{
    public class AuthorFormModel
    {
        [Required]
        [MaxLength(AuthorConstants.AuthorNameMaxLength)]
        public string Name { get; set; }
        [Required]
        [MaxLength(AuthorConstants.AuthorDescriptionMaxLength)]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}

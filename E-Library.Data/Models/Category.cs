using E_Library.Data.Constants;
using System.ComponentModel.DataAnnotations;

namespace E_Library.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(CategoryConstants.CategoryNameMaxLength)]
        public string Name { get; set; }
    }
}

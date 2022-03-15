using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Library.Data.Models
{
    public class Cart
    {
        [Key]
        [StringLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string UserId { get; set; }
        [Required]
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public ICollection<BookCart> CartBooks { get; set; } = new List<BookCart>();
    }
}
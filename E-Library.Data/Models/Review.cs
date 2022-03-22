using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Library.Data.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
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

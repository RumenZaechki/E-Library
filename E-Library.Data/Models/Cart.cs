using System.ComponentModel.DataAnnotations;

namespace E_Library.Data.Models
{
    public class Cart
    {
        [Key]
        [StringLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public User User { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
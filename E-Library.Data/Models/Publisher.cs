using System.ComponentModel.DataAnnotations;

namespace E_Library.Data.Models
{
    public class Publisher
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}

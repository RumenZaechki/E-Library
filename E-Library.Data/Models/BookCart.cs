
namespace E_Library.Data.Models
{
    public class BookCart
    {
        public string BookId { get; set; }
        public Book Book { get; set; }
        public string CartId { get; set; }
        public Cart Cart { get; set; }
    }
}

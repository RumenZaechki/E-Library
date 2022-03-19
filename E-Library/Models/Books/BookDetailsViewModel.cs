namespace E_Library.Models.Books
{
    public class BookDetailsViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string ImageUrl { get; set; }
        public int Release { get; set; }
        public string AuthorId { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
    }
}

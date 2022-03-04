namespace E_Library.Models.Books
{
    public class BookListingViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int Release { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
    }
}

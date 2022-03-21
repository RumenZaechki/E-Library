namespace E_Library.Services.Books.Models
{
    public class BookServiceModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int Release { get; set; }
        public string AuthorId { get; set; }
        public string Author { get; set; }
        public string AuthorDescription { get; set; }
        public string AuthorImage { get; set; }
        public string PublisherId { get; set; }
        public string Publisher { get; set; }
        public string Category { get; set; }
    }
}
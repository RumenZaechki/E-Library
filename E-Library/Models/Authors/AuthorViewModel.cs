namespace E_Library.Models.Authors
{
    public class AuthorViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<BookViewModel> Books { get; set; } = new List<BookViewModel>();
    }
}

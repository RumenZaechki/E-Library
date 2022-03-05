using E_Library.Models.Books;

namespace E_Library.Models.Home
{
    public class IndexViewModel
    {
        public int TotalBooks { get; set; }
        public int TotalUsers { get; set; }
        public IEnumerable<BookListingViewModel> RecentlyAddedBooks { get; set; }
    }
}

using E_Library.Models.Books;

namespace E_Library.Models.Home
{
    public class IndexViewModel
    {
        public IEnumerable<BookListingViewModel> RecentlyAddedBooks { get; set; }
    }
}

namespace E_Library.Models.Books
{
    public class AllBooksQueryModel
    {
        public BookSorting BookSorting { get; set; }
        public string SearchTerm { get; set; }
        public IEnumerable<BookListingViewModel> AllBooks { get; set; }
    }
}

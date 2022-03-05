using System.ComponentModel;

namespace E_Library.Models.Books
{
    public class AllBooksQueryModel
    {
        [DisplayName("Select Category")]
        public string SelectedCategory { get; set; }
        public Dictionary<int,string> Categories { get; set; }
        public string SearchTerm { get; set; }
        public IEnumerable<BookListingViewModel> AllBooks { get; set; }
    }
}

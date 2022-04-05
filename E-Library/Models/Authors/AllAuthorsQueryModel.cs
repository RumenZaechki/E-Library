namespace E_Library.Models.Authors
{
    public class AllAuthorsQueryModel
    {
        public int AuthorsPerPage { get; set; } = 3;
        public int CurrentPage { get; set; } = 1;
        public string SearchTerm { get; set; }
        public IEnumerable<AuthorListingViewModel> AllAuthors { get; set; }
        public int AuthorsCount { get; set; }
    }
}

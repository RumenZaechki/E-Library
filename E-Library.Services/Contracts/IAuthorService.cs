using E_Library.Services.Authors.Models;

namespace E_Library.Services.Contracts
{
    public interface IAuthorService
    {
        public int GetAuthorsCount(string searchTerm);
        public IEnumerable<AuthorServiceModel> GetAuthors(int currentPage, int authorsPerPage, string searchTerm);
        public void Add(string name, string description, string imageUrl);
        public AuthorServiceModel GetAuthor(string id);
        public void Edit(string id, string name, string description, string imageUrl);
    }
}

using E_Library.Services.Authors.Models;

namespace E_Library.Services.Contracts
{
    public interface IAuthorService
    {
        public Task<int> GetAuthorsCountAsync(string searchTerm);
        public Task<IEnumerable<AuthorServiceModel>> GetAuthorsAsync(int currentPage, int authorsPerPage, string searchTerm);
        public Task AddAsync(string name, string description, string imageUrl);
        public Task<AuthorServiceModel> GetAuthorAsync(string id);
        public Task EditAsync(string id, string name, string description, string imageUrl);
    }
}

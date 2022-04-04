using E_Library.Services.Authors.Models;

namespace E_Library.Services.Contracts
{
    public interface IAuthorService
    {
        public AuthorServiceModel GetAuthor(string id);
        public void Edit(string id, string name, string description, string imageUrl);
    }
}

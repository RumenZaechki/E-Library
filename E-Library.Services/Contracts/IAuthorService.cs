using E_Library.Services.Authors.Models;

namespace E_Library.Services.Contracts
{
    public interface IAuthorService
    {
        public AuthorServiceModel GetAuthor(string id);
    }
}

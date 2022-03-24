using E_Library.Data;
using E_Library.Data.Models;
using E_Library.Services.Authors.Models;
using E_Library.Services.Contracts;

namespace E_Library.Services.Authors
{
    public class AuthorService : IAuthorService
    {
        private readonly LibraryDbContext data;
        public AuthorService(LibraryDbContext data)
        {
            this.data = data;
        }
        public AuthorServiceModel GetAuthor(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            Author author = this.data.Authors
                .FirstOrDefault(a => a.Id == id);

            var books = this.data.Books
                .Where(a => a.AuthorId == id)
                .Select(b => new AuthorBookServiceModel
                {
                    Id = b.Id,
                    Title = b.Title
                })
                .ToList();

            return new AuthorServiceModel
            {
                Name = author.Name,
                Description = author.Description,
                ImageUrl = author.ImageUrl,
                Books = books
            };
        }
    }
}

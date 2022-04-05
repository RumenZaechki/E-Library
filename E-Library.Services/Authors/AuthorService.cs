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
        public void Add(string name, string description, string imageUrl)
        {
            if (this.data.Authors.Any(a => a.Name == name))
            {
                return;
            }
            var author = new Author
            {
                Name = name,
                Description = description,
                ImageUrl = imageUrl
            };
            this.data.Authors.Add(author);
            this.data.SaveChanges();
        }
        public AuthorServiceModel GetAuthor(string id)
        {
            Author author = this.data.Authors
                .FirstOrDefault(a => a.Id == id);

            if (author == null)
            {
                var model = new AuthorServiceModel();
                return model;
            }

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
                Id = author.Id,
                Name = author.Name,
                Description = author.Description,
                ImageUrl = author.ImageUrl,
                Books = books
            };
        }

        public void Edit(string id, string name, string description, string imageUrl)
        {
            var author = this.data.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null)
            {
                return;
            }
            author.Name = name;
            author.Description = description;
            author.ImageUrl = imageUrl;
            author.Books = this.data.Books.Where(b => b.AuthorId == id).ToList();
            this.data.Authors.Update(author);
            this.data.SaveChanges();
        }
    }
}

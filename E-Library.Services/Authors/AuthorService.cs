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
        public int GetAuthorsCount(string searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return this.data.Authors
                    .Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()))
                    .Count();
            }
            return this.data.Authors
                .Count();
        }
        public IEnumerable<AuthorServiceModel> GetAuthors(int currentPage, int authorsPerPage, string searchTerm)
        {
            if (currentPage <= 0)
            {
                currentPage = 1;
            }
            if (authorsPerPage <= 0)
            {
                authorsPerPage = 3;
            }
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return this.data.Authors
                    .Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()))
                    .Skip((currentPage - 1) * authorsPerPage)
                    .Take(authorsPerPage)
                    .Select(a => new AuthorServiceModel
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Description = a.Description,
                        ImageUrl = a.ImageUrl
                    })
                    .ToList();
            }
            return this.data.Authors
                .Skip((currentPage - 1) * authorsPerPage)
                .Take(authorsPerPage)
                .Select(a => new AuthorServiceModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    ImageUrl = a.ImageUrl
                })
                .ToList();
        }
        public void Add(string name, string description, string imageUrl)
        {
            if (this.data.Authors.Any(a => a.Name == name) || !IsValid(name, description, imageUrl))
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
            if (author == null || !IsValid(name, description, imageUrl))
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

        private bool IsValid(string name, string description, string imageUrl)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description) || string.IsNullOrWhiteSpace(imageUrl))
            {
                return false;
            }
            return true;
        }
    }
}

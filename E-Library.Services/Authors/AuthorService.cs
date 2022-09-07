using E_Library.Data;
using E_Library.Data.Models;
using E_Library.Services.Authors.Models;
using E_Library.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace E_Library.Services.Authors
{
    public class AuthorService : IAuthorService
    {
        private readonly LibraryDbContext data;
        public AuthorService(LibraryDbContext data)
        {
            this.data = data;
        }
        public async Task<int> GetAuthorsCountAsync(string searchTerm)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                return await this.data.Authors
                    .Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()))
                    .CountAsync();
            }
            return await this.data.Authors
                .CountAsync();
        }
        public async Task<IEnumerable<AuthorServiceModel>> GetAuthorsAsync(int currentPage, int authorsPerPage, string searchTerm)
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
                return await this.data.Authors
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
                    .ToListAsync();
            }
            return await this.data.Authors
                .Skip((currentPage - 1) * authorsPerPage)
                .Take(authorsPerPage)
                .Select(a => new AuthorServiceModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    ImageUrl = a.ImageUrl
                })
                .ToListAsync();
        }
        public async Task AddAsync(string name, string description, string imageUrl)
        {
            if (!await this.data.Authors.AnyAsync(a => a.Name == name) && IsValid(name, description, imageUrl))
            {
                var author = new Author
                {
                    Name = name,
                    Description = description,
                    ImageUrl = imageUrl
                };
                this.data.Authors.Add(author);
                await this.data.SaveChangesAsync();
            }
        }
        public async Task<AuthorServiceModel> GetAuthorAsync(string id)
        {
            Author author = await this.data.Authors
                .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
            {
                return null;
            }

            var books = await this.data.Books
                .Where(a => a.AuthorId == id)
                .Select(b => new AuthorBookServiceModel
                {
                    Id = b.Id,
                    Title = b.Title
                })
                .ToListAsync();

            return new AuthorServiceModel
            {
                Id = author.Id,
                Name = author.Name,
                Description = author.Description,
                ImageUrl = author.ImageUrl,
                Books = books
            };
        }

        public async Task EditAsync(string id, string name, string description, string imageUrl)
        {
            var author = await this.data.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if (author != null && IsValid(name, description, imageUrl))
            {
                author.Name = name;
                author.Description = description;
                author.ImageUrl = imageUrl;
                author.Books = await this.data.Books.Where(b => b.AuthorId == id).ToListAsync();
                this.data.Authors.Update(author);
                await this.data.SaveChangesAsync();
            }
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

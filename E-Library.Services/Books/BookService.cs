using E_Library.Data;
using E_Library.Data.Models;
using E_Library.Services.Books.Models;
using E_Library.Services.Contracts;

namespace E_Library.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext data;
        public BookService(LibraryDbContext data)
        {
            this.data = data;
        }
        public IEnumerable<BookServiceModel> FindBooks(string searchTerm)
        {
            return this.data.Books
                .Where(b => b.Title.ToLower().Contains(searchTerm.ToLower()))
                .Select(b => new BookServiceModel
                {
                    Title = b.Title,
                    Description = b.Description,
                    Price = b.Price,
                    ImageUrl = b.ImageUrl,
                    Release = b.Release,
                    Author = b.Author.Name,
                    Category = b.Category.Name
                });
        }
        public int GetTotalBooks()
        {
            return this.data.Books.Count();
        }
        public int GetTotalUsers()
        {
            return this.data.Users.Count();
        }
        public IEnumerable<BookServiceModel> GetBooks()
        {
            return this.data.Books
                .Select(x => new BookServiceModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Price = x.Price,
                    ImageUrl = x.ImageUrl,
                    Release = x.Release,
                    Author = x.Author.Name,
                    Category = x.Category.Name
                })
                .ToList();
        }
        public void Create(string title, string description, decimal price, string imageUrl, int release, string author, int categoryId)
        {
            Author authorToAdd = null;
            if (!this.data.Authors.Any(a => a.Name == author))
            {
                authorToAdd = new Author
                {
                    Name = author
                };
                this.data.Authors.Add(authorToAdd);
                this.data.SaveChanges();
            }
            authorToAdd = this.data.Authors.Where(a => a.Name == author).FirstOrDefault();
            var bookToAdd = new Book
            {
                Title = title,
                Description = description,
                Price = price,
                ImageUrl = imageUrl,
                Release = release,
                CategoryId = categoryId,
                AuthorId = authorToAdd.Id,
            };
            this.data.Books.Add(bookToAdd);
            this.data.SaveChanges();
        }
        public Dictionary<int, string> GetBookCategories()
        {
            var data =  this.data
                .Categories
                .Select(c => new 
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToList();
            var dict = data.ToDictionary(c => c.Id, c => c.Name);
            return dict;
        }
    }
}
using E_Library.Data;
using E_Library.Data.Models;
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
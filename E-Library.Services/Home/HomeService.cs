using E_Library.Data;
using E_Library.Services.Books.Models;
using E_Library.Services.Contracts;

namespace E_Library.Services.Home
{
    public class HomeService : IHomeService
    {
        private readonly LibraryDbContext data;
        public HomeService(LibraryDbContext data)
        {
            this.data = data;
        }
        public IEnumerable<BookServiceModel> GetBooks()
        {
            return this.data.Books
                .Take(3)
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

        public int GetTotalUsers()
        {
            return this.data.Users.Count();
        }
        public int GetTotalBooks()
        {
            return this.data.Books.Count();
        }
    }
}
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
        public List<BookServiceModel> GetRecentBooks()
        {
            return this.data.Books
                .OrderByDescending(x => x.CreatedOn)
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
                    //Category = x.Category.Name
                }).
                ToList();
        }
    }
}
using E_Library.Data;
using E_Library.Data.Models;
using E_Library.Services.Contracts;
using E_Library.Services.Publishers.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Library.Services.Publishers
{
    public class PublishersService : IPublishersService
    {
        private readonly LibraryDbContext data;
        public PublishersService(LibraryDbContext data)
        {
            this.data = data;
        }
        public async Task<PublisherServiceModel> DetailsAsync(string publisherId)
        {
            Publisher publisher = await this.data.Publishers
                .FirstOrDefaultAsync(p => p.Id == publisherId);

            if (publisher == null)
            {
                return null;
            }

            var books = this.data.Books
                .Where(p => p.PublisherId == publisherId);

            var booksService = await books
                .Select(b => new PublisherBookServiceModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    ImageUrl = b.ImageUrl
                })
                .ToListAsync();

            var authorsService = await books
                .Select(b => new PublisherAuthorServiceModel
                {
                    Id = b.AuthorId,
                    Name = b.Author.Name
                })
                .Distinct()
                .ToListAsync();

            PublisherServiceModel model = new PublisherServiceModel
            {
                Name = publisher.Name,
                Books = booksService,
                Authors = authorsService
            };
            return model;
        }
    }
}

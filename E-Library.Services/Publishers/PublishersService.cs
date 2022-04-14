using E_Library.Data;
using E_Library.Data.Models;
using E_Library.Services.Contracts;
using E_Library.Services.Publishers.Models;

namespace E_Library.Services.Publishers
{
    public class PublishersService : IPublishersService
    {
        private readonly LibraryDbContext data;
        public PublishersService(LibraryDbContext data)
        {
            this.data = data;
        }
        public PublisherServiceModel Details(string publisherId)
        {
            Publisher publisher = this.data.Publishers
                .FirstOrDefault(p => p.Id == publisherId);

            if (publisher == null)
            {
                return null;
            }

            var books = this.data.Books
                .Where(p => p.PublisherId == publisherId);

            var booksService = books
                .Select(b => new PublisherBookServiceModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    ImageUrl = b.ImageUrl
                })
                .ToList();

            var authorsService = books
                .Select(b => new PublisherAuthorServiceModel
                {
                    Id = b.AuthorId,
                    Name = b.Author.Name
                })
                .Distinct()
                .ToList();

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

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

            var authors = this.data.Books
                .Where(b => b.PublisherId == publisherId)
                .Select(b => b.Author);

            if (publisher.Books.Count == 0 && books.Count() > 0)
            {
                publisher.Books
                    .ToList()
                    .AddRange(books);
                this.data.SaveChanges();
            }

            if (publisher.Authors.Count == 0 && authors.Count() > 0)
            {
                publisher.Authors
                    .ToList()
                    .AddRange(authors);
                this.data.SaveChanges();
            }

            var booksService = books
                .Select(b => new PublisherBookServiceModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    ImageUrl = b.ImageUrl
                })
                .ToList();

            var authorsService = authors
                .Select(b => new PublisherAuthorServiceModel
                {
                    Id = b.Id,
                    Name = b.Name
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

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

        public int GetBooksCount()
        {
            return this.data.Books.Count();
        }

        public Dictionary<int, string> GetBookCategories()
        {
            var data = this.data
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

        public void Create(string title, string description, decimal price, string imageUrl, int release, string author, string authorDescription, string authorImage, string publisher, int categoryId)
        {
            Author authorToAdd = null;
            if (!this.data.Authors.Any(a => a.Name == author))
            {
                authorToAdd = new Author
                {
                    Name = author,
                    Description = authorDescription,
                    ImageUrl = authorImage
                };
                this.data.Authors.Add(authorToAdd);
                this.data.SaveChanges();
            }
            authorToAdd = this.data.Authors
                .Where(a => a.Name == author)
                .FirstOrDefault();

            Publisher publisherToAdd = null;
            if (!this.data.Publishers.Any(a => a.Name == publisher))
            {
                publisherToAdd = new Publisher
                {
                    Name = publisher
                };
                this.data.Publishers.Add(publisherToAdd);
                this.data.SaveChanges();
            }
            publisherToAdd = this.data.Publishers
                .Where(p => p.Name == publisher)
                .FirstOrDefault();

            var bookToAdd = new Book
            {
                Title = title,
                Description = description,
                Price = price,
                ImageUrl = imageUrl,
                Release = release,
                CategoryId = categoryId,
                AuthorId = authorToAdd.Id,
                Author = authorToAdd,
                PublisherId = publisherToAdd.Id,
                Publisher = publisherToAdd
            };
            this.data.Books.Add(bookToAdd);
            this.data.SaveChanges();
        }

        public IEnumerable<BookServiceModel> FindBooks(string searchTerm, int currentPage, int booksPerPage)
        {
            if (searchTerm != null)
            {
                return this.data.Books
                    .Where(b => b.Title.ToLower().Contains(searchTerm.ToLower()))
                    .Skip((currentPage - 1) * booksPerPage)
                    .Take(booksPerPage)
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
            else
            {
                return this.data.Books
                    .Skip((currentPage - 1) * booksPerPage)
                    .Take(booksPerPage)
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
        }

        public IEnumerable<BookServiceModel> FindBooksThatMatchCategory(string category)
        {
            return this.data.Books
                .Where(b => b.Category.Name == category)
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

        public BookServiceModel Details(string id)
        {
            return this.data.Books
                .Where(x => x.Id == id)
                .Select(b => new BookServiceModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    Price = b.Price,
                    ImageUrl = b.ImageUrl,
                    Release = b.Release,
                    AuthorId = b.Author.Id,
                    AuthorDescription = b.Author.Description,
                    AuthorImage = b.Author.ImageUrl,
                    Author = b.Author.Name,
                    PublisherId = b.PublisherId,
                    Publisher = b.Publisher.Name,
                    Category = b.Category.Name
                })
                .FirstOrDefault();
        }


        public void Edit(string id, string title, string description, decimal price, string imageUrl, int release, string author, string authorDescription, string authorImage, string publisher, int categoryId)
        {
            Book book = this.data.Books.FirstOrDefault(b => b.Id == id);
            Author authorToEdit = this.data.Authors.FirstOrDefault(a => a.Id == book.AuthorId);


            book.Title = title;
            book.Description = description;
            book.Price = price;
            book.ImageUrl = imageUrl;
            book.Release = release;
            book.CategoryId = categoryId;
            book.Author.Name = author;
            authorToEdit.Name = author;
            authorToEdit.Description = authorDescription;
            authorToEdit.ImageUrl = authorImage;
            book.Publisher.Name = publisher;
            this.data.Books.Update(book);
            this.data.Authors.Update(authorToEdit);
            this.data.SaveChanges();
        }
        public void Delete(string id)
        {
            Book book = this.data.Books.FirstOrDefault(b => b.Id == id);
            this.data.Books.Remove(book);
            this.data.SaveChanges();
        }
    }
}
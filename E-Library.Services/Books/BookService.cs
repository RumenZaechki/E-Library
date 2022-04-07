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

        public int GetBooksCount(string searchTerm, string selectedCategory)
        {
            int count = 0;
            if (!string.IsNullOrWhiteSpace(searchTerm) && !string.IsNullOrWhiteSpace(selectedCategory))
            {
                return this.data.Books
                    .Where(b => b.Title.ToLower().Contains(searchTerm.ToLower()) && b.Category.Name.ToLower() == selectedCategory.ToLower())
                    .Count();
            }
            if (!string.IsNullOrWhiteSpace(selectedCategory))
            {
                count += FindBooksThatMatchCategory(selectedCategory)
                        .Count();
            }
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                count += this.data.Books
                        .Where(b => b.Title.ToLower().Contains(searchTerm.ToLower()))
                        .Count();
            }
            if (!string.IsNullOrWhiteSpace(searchTerm) || !string.IsNullOrWhiteSpace(selectedCategory))
            {
                return count;
            }
            return this.data.Books.Count();
        }

        public IEnumerable<CategoryServiceModel> GetBookCategories()
        {
            return this.data
                .Categories
                .Select(c => new CategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToList();
        }

        public void Create(string title, string description, decimal price, string imageUrl, int release, string author, string publisher, int categoryId)
        {
            if (IsValid(title, description, price, imageUrl, release, author, publisher, categoryId))
            {
                Author authorToAdd = null;
                if (!this.data.Authors.Any(a => a.Name == author))
                {
                    authorToAdd = new Author
                    {
                        Name = author,
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

                Book bookToAdd = new Book
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
        }


        public IEnumerable<BookServiceModel> FindBooks(string searchTerm, string selectedCategory, int currentPage, int booksPerPage)
        {
            if (currentPage <= 0)
            {
                currentPage = 1;
            }
            if (booksPerPage <= 0)
            {
                booksPerPage = 3;
            }
            if (!string.IsNullOrWhiteSpace(selectedCategory) && !string.IsNullOrWhiteSpace(searchTerm))
            {
                var books = this.data.Books
                    .Where(b => b.Title.ToLower().Contains(searchTerm.ToLower()) && b.Category.Name.ToLower() == selectedCategory.ToLower())
                    .Skip((currentPage - 1) * booksPerPage)
                    .Take(booksPerPage)
                    .Select(b => new BookServiceModel
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Description = b.Description,
                        Price = b.Price,
                        ImageUrl = b.ImageUrl,
                        Release = b.Release,
                        Author = b.Author.Name,
                        Category = b.Category.Name
                    })
                    .ToList();
                return books;
            }
            else if (!string.IsNullOrWhiteSpace(selectedCategory) || !string.IsNullOrWhiteSpace(searchTerm))
            {
                var booksFromSelectedCategory = string.IsNullOrWhiteSpace(selectedCategory) ? null : FindBooksThatMatchCategory(selectedCategory)
                                                .ToList();
                var booksFromSearchTerm = string.IsNullOrWhiteSpace(searchTerm) ? null : this.data.Books
                        .Where(b => b.Title.ToLower().Contains(searchTerm.ToLower()))
                        .Skip((currentPage - 1) * booksPerPage)
                        .Take(booksPerPage)
                        .Select(b => new BookServiceModel
                        {
                            Id = b.Id,
                            Title = b.Title,
                            Description = b.Description,
                            Price = b.Price,
                            ImageUrl = b.ImageUrl,
                            Release = b.Release,
                            Author = b.Author.Name,
                            Category = b.Category.Name
                        })
                        .ToList();
                var result = booksFromSearchTerm ?? Enumerable.Empty<BookServiceModel>().Union(booksFromSelectedCategory ?? Enumerable.Empty<BookServiceModel>());
                return result;
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

        public void Edit(string id, string title, string description, decimal price, string imageUrl, int release, string author, string publisher, int categoryId)
        {
            Book book = this.data.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {

                Author authorToEdit = this.data.Authors.FirstOrDefault(a => a.Id == book.AuthorId);
                Publisher publisherToEdit = this.data.Publishers.FirstOrDefault(p => p.Id == book.PublisherId);
                if (authorToEdit != null && publisherToEdit != null)
                {

                    if (authorToEdit.Name != author)
                    {
                        authorToEdit = this.data.Authors.FirstOrDefault(a => a.Name == author);
                        if (authorToEdit == null)
                        {
                            authorToEdit = new Author
                            {
                                Name = author,
                            };
                            this.data.Authors.Add(authorToEdit);
                            this.data.SaveChanges();
                        }
                    }

                    if (publisherToEdit.Name != publisher)
                    {
                        publisherToEdit = this.data.Publishers.FirstOrDefault(p => p.Name == publisher);
                        if (publisherToEdit == null)
                        {
                            publisherToEdit = new Publisher
                            {
                                Name = author
                            };
                            this.data.Publishers.Add(publisherToEdit);
                            this.data.SaveChanges();
                        }
                    }

                    book.Title = title;
                    book.Description = description;
                    book.Price = price;
                    book.ImageUrl = imageUrl;
                    book.Release = release;
                    book.CategoryId = categoryId;
                    book.Author = authorToEdit;
                    book.Publisher = publisherToEdit;
                    this.data.Books.Update(book);
                    this.data.Authors.Update(authorToEdit);
                    this.data.Publishers.Update(publisherToEdit);
                    this.data.SaveChanges();
                }
            }
        }
        public void Delete(string id)
        {
            Book book = this.data.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                this.data.Books.Remove(book);
                this.data.SaveChanges();
            }
        }
        private bool IsValid(string title, string description, decimal price, string imageUrl, int release, string author, string publisher, int categoryId)
        {
            string[] arr = new string[] { title, description, imageUrl, author, publisher };
            if (arr.Any(x => string.IsNullOrWhiteSpace(x)) || price < 0m || !this.data.Categories.Any(c => c.Id == categoryId) || release < 0)
            {
                return false;
            }
            if (this.data.Books.Any(b => b.Title == title))
            {
                return false;
            }
            return true;
        }
        private IEnumerable<BookServiceModel> FindBooksThatMatchCategory(string category)
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
    }
}
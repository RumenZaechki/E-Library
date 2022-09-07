using E_Library.Data;
using E_Library.Data.Models;
using E_Library.Services.Books.Models;
using E_Library.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace E_Library.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext data;
        public BookService(LibraryDbContext data)
        {
            this.data = data;
        }

        public async Task<int> GetBooksCountAsync(string searchTerm, string selectedCategory)
        {
            int count = 0;
            if (!string.IsNullOrWhiteSpace(searchTerm) && !string.IsNullOrWhiteSpace(selectedCategory))
            {
                return await this.data.Books
                    .Where(b => b.Title.ToLower().Contains(searchTerm.ToLower()) && b.Category.Name.ToLower() == selectedCategory.ToLower())
                    .CountAsync();
            }
            if (!string.IsNullOrWhiteSpace(selectedCategory))
            {
                count += FindBooksThatMatchCategory(selectedCategory)
                        .Count();
            }
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                count += await this.data.Books
                        .Where(b => b.Title.ToLower().Contains(searchTerm.ToLower()))
                        .CountAsync();
            }
            if (!string.IsNullOrWhiteSpace(searchTerm) || !string.IsNullOrWhiteSpace(selectedCategory))
            {
                return count;
            }
            return await this.data.Books.CountAsync();
        }

        public async Task<IEnumerable<CategoryServiceModel>> GetBookCategoriesAsync()
        {
            return await this.data
                .Categories
                .Select(c => new CategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();
        }

        public async Task CreateAsync(string title, string description, decimal price, string imageUrl, int release, string author, string publisher, int categoryId)
        {
            if (IsValid(title, description, price, imageUrl, release, author, publisher, categoryId))
            {
                Author authorToAdd = null;
                if (!await this.data.Authors.AnyAsync(a => a.Name == author))
                {
                    authorToAdd = new Author
                    {
                        Name = author,
                    };
                    this.data.Authors.Add(authorToAdd);
                    await this.data.SaveChangesAsync();
                }
                authorToAdd = await this.data.Authors
                    .Where(a => a.Name == author)
                    .FirstOrDefaultAsync();

                Publisher publisherToAdd = null;
                if (!await this.data.Publishers.AnyAsync(a => a.Name == publisher))
                {
                    publisherToAdd = new Publisher
                    {
                        Name = publisher
                    };
                    this.data.Publishers.Add(publisherToAdd);
                    await this.data.SaveChangesAsync();
                }
                publisherToAdd = await this.data.Publishers
                    .Where(p => p.Name == publisher)
                    .FirstOrDefaultAsync();

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
                await this.data.SaveChangesAsync();
            }
        }


        public async Task<IEnumerable<BookServiceModel>> FindBooksAsync(string searchTerm, string selectedCategory, int currentPage, int booksPerPage)
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
                var books = await this.data.Books
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
                    .ToListAsync();
                return books;
            }
            else if (!string.IsNullOrWhiteSpace(selectedCategory) || !string.IsNullOrWhiteSpace(searchTerm))
            {
                var booksFromSelectedCategory = string.IsNullOrWhiteSpace(selectedCategory) ? null : FindBooksThatMatchCategory(selectedCategory)
                                                .ToList();
                var booksFromSearchTerm = string.IsNullOrWhiteSpace(searchTerm) ? null : await this.data.Books
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
                        .ToListAsync();
                var result = booksFromSearchTerm ?? Enumerable.Empty<BookServiceModel>().Union(booksFromSelectedCategory ?? Enumerable.Empty<BookServiceModel>());
                return result;
            }
            else
            {
                return await this.data.Books
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
                    .ToListAsync();
            }
        }

        public async Task<BookServiceModel> DetailsAsync(string id)
        {
            return await this.data.Books
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
                .FirstOrDefaultAsync();
        }

        public async Task EditAsync(string id, string title, string description, decimal price, string imageUrl, int release, string author, string publisher, int categoryId)
        {
            Book book = await this.data.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book != null)
            {

                Author authorToEdit = await this.data.Authors.FirstOrDefaultAsync(a => a.Id == book.AuthorId);
                Publisher publisherToEdit = await this.data.Publishers.FirstOrDefaultAsync(p => p.Id == book.PublisherId);
                if (authorToEdit != null && publisherToEdit != null)
                {

                    if (authorToEdit.Name != author)
                    {
                        authorToEdit = await this.data.Authors.FirstOrDefaultAsync(a => a.Name == author);
                        if (authorToEdit == null)
                        {
                            authorToEdit = new Author
                            {
                                Name = author,
                            };
                            this.data.Authors.Add(authorToEdit);
                            await this.data.SaveChangesAsync();
                        }
                    }

                    if (publisherToEdit.Name != publisher)
                    {
                        publisherToEdit = await this.data.Publishers.FirstOrDefaultAsync(p => p.Name == publisher);
                        if (publisherToEdit == null)
                        {
                            publisherToEdit = new Publisher
                            {
                                Name = author
                            };
                            this.data.Publishers.Add(publisherToEdit);
                            await this.data.SaveChangesAsync();
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
                    await this.data.SaveChangesAsync();
                }
            }
        }
        public async Task DeleteAsync(string id)
        {
            Book book = await this.data.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book != null)
            {
                this.data.Books.Remove(book);
                await this.data.SaveChangesAsync();
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
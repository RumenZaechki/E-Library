using E_Library.Data.Models;
using E_Library.Infrastructure;
using E_Library.Services;
using E_Library.Services.Books.Models;
using E_Library.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace E_Library.Test.Services
{
    public class BookServiceTests
    {
        [Fact]
        public void FindBooksReturnsOneBookWhenGivenOnlySearchTerm()
        {
            var data = DbMock.Instance;
            CategorySeeder.SeedCategories(data);
            PublisherSeeder.SeedPublishers(data);
            AuthorSeeder.SeedAuthors(data);
            BookSeeder.SeedBooks(data);
            var booksService = new BookService(data);

            var result = booksService.FindBooks("lord", "", 1, 3).ToList();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(1, result.Count());
            Assert.Equal("Lord Of The Rings", result[0].Title);
        }

        [Fact]
        public void FindBooksReturnsTwoBooksWhenGivenOnlyCategory()
        {
            var data = DbMock.Instance;
            CategorySeeder.SeedCategories(data);
            PublisherSeeder.SeedPublishers(data);
            AuthorSeeder.SeedAuthors(data);
            BookSeeder.SeedBooks(data);
            var booksService = new BookService(data);

            var result = booksService.FindBooks("", "Fantasy and science fiction", 1, 3).ToList();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("The Hitchhiker's Guide to the Galaxy", result[0].Title);
            Assert.Equal("Lord Of The Rings", result[1].Title);
        }

        [Fact]
        public void FindBooksReturnsOneBookWhenGivenCorrectInput()
        {
            var data = DbMock.Instance;
            CategorySeeder.SeedCategories(data);
            PublisherSeeder.SeedPublishers(data);
            AuthorSeeder.SeedAuthors(data);
            BookSeeder.SeedBooks(data);
            var booksService = new BookService(data);

            var result = booksService.FindBooks("lord", "Fantasy and science fiction", 1, 3).ToList();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(1, result.Count());
            Assert.Equal("Lord Of The Rings", result[0].Title);
        }

        [Fact]
        public void FindBooksReturnsAllBooksWhenGivenIncorrectInput()
        {
            var data = DbMock.Instance;
            CategorySeeder.SeedCategories(data);
            PublisherSeeder.SeedPublishers(data);
            AuthorSeeder.SeedAuthors(data);
            BookSeeder.SeedBooks(data);
            var booksService = new BookService(data);

            var result = booksService.FindBooks("", "", 0, 0);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void EditWorksCorrectlyWhenGivenCorrectInput()
        {
            var data = DbMock.Instance;
            CategorySeeder.SeedCategories(data);
            PublisherSeeder.SeedPublishers(data);
            AuthorSeeder.SeedAuthors(data);
            var book = GetBook();
            data.Books.Add(book);
            data.SaveChanges();
            var booksService = new BookService(data);

            booksService.Edit(book.Id, book.Title, book.Description, 19m, book.ImageUrl, book.Release, book.Author.Name, book.Publisher.Name, book.CategoryId);
            var result = data.Books.FirstOrDefault();

            Assert.Equal(book.Title, result.Title);
            Assert.Equal(book.Description, result.Description);
            Assert.Equal(19m, result.Price);
            Assert.Equal(book.ImageUrl, result.ImageUrl);
            Assert.Equal(book.Release, result.Release);
            Assert.Equal(book.Author.Name, result.Author.Name);
            Assert.Equal(book.Author.Description, result.Author.Description);
            Assert.Equal(book.Author.ImageUrl, result.Author.ImageUrl);
            Assert.Equal(book.Publisher.Name, result.Publisher.Name);
        }

        [Fact]
        public void EditDoesNothingWhenGivenIncorrectPublisherInput()
        {
            var data = DbMock.Instance;
            CategorySeeder.SeedCategories(data);
            PublisherSeeder.SeedPublishers(data);
            AuthorSeeder.SeedAuthors(data);
            var book = GetBook();
            data.Books.Add(book);
            data.SaveChanges();
            var booksService = new BookService(data);

            booksService.Edit(book.Id, book.Title, book.Description, book.Price, book.ImageUrl, book.Release, book.Author.Name, "incorrectPublisher", book.CategoryId);
            var result = data.Books.FirstOrDefault();

            Assert.Equal(book.Title, result.Title);
            Assert.Equal(book.Description, result.Description);
            Assert.Equal(book.Price, result.Price);
            Assert.Equal(book.ImageUrl, result.ImageUrl);
            Assert.Equal(book.Release, result.Release);
            Assert.Equal(book.Author.Name, result.Author.Name);
            Assert.Equal(book.Author.Description, result.Author.Description);
            Assert.Equal(book.Author.ImageUrl, result.Author.ImageUrl);
            Assert.Equal(book.Publisher.Name, result.Publisher.Name);
        }

        [Fact]
        public void EditDoesNothingWhenGivenIncorrectAuthorInput()
        {
            var data = DbMock.Instance;
            CategorySeeder.SeedCategories(data);
            PublisherSeeder.SeedPublishers(data);
            AuthorSeeder.SeedAuthors(data);
            var book = GetBook();
            data.Books.Add(book);
            data.SaveChanges();
            var booksService = new BookService(data);

            booksService.Edit(book.Id, book.Title, book.Description, book.Price, book.ImageUrl, book.Release, "incorrectAuthor", book.Publisher.Name, book.CategoryId);
            var result = data.Books.FirstOrDefault();

            Assert.Equal(book.Title, result.Title);
            Assert.Equal(book.Description, result.Description);
            Assert.Equal(book.Price, result.Price);
            Assert.Equal(book.ImageUrl, result.ImageUrl);
            Assert.Equal(book.Release, result.Release);
            Assert.Equal(book.Author.Name, result.Author.Name);
            Assert.Equal(book.Author.Description, result.Author.Description);
            Assert.Equal(book.Author.ImageUrl, result.Author.ImageUrl);
            Assert.Equal(book.Publisher.Name, result.Publisher.Name);
        }

        [Fact]
        public void EditDoesNothingWhenGivenIncorrectInput()
        {
            var data = DbMock.Instance;
            CategorySeeder.SeedCategories(data);
            PublisherSeeder.SeedPublishers(data);
            AuthorSeeder.SeedAuthors(data);
            var book = GetBook();
            data.Books.Add(book);
            data.SaveChanges();
            var booksService = new BookService(data);

            booksService.Edit("incorrectId", "incorrectTitle", "incorrectDescription", 0m, "incorrectImageUrl", 0, "incorrectAuthor", "IncorrectPublisher", 0);
            var result = data.Books.FirstOrDefault();

            Assert.Equal(book.Title, result.Title);
            Assert.Equal(book.Description, result.Description);
            Assert.Equal(book.Price, result.Price);
            Assert.Equal(book.ImageUrl, result.ImageUrl);
            Assert.Equal(book.Release, result.Release);
            Assert.Equal(book.Author.Name, result.Author.Name);
            Assert.Equal(book.Author.Description, result.Author.Description);
            Assert.Equal(book.Author.ImageUrl, result.Author.ImageUrl);
            Assert.Equal(book.Publisher.Name, result.Publisher.Name);
        }

        [Fact]
        public void DeleteWorksCorrectlyWhenGivenCorrectInput()
        {
            var data = DbMock.Instance;
            CategorySeeder.SeedCategories(data);
            var book = GetBook();
            data.Books.Add(book);
            data.SaveChanges();
            Assert.Equal(1, data.Books.Count());
            var booksService = new BookService(data);

            booksService.Delete(book.Id);

            Assert.Empty(data.Books);
        }

        [Fact]
        public void DeleteDoesNothingWhenGivenIncorrectInput()
        {
            var data = DbMock.Instance;
            CategorySeeder.SeedCategories(data);
            var book = GetBook();
            data.Books.Add(book);
            data.SaveChanges();
            var booksService = new BookService(data);

            booksService.Delete("incorrectId");

            Assert.Equal(1, data.Books.Count());
        }

        [Fact]
        public void GetBooksCountShouldReturnCountTwoWhenGivenOnlyCorrectSelectedCategory()
        {
            var data = DbMock.Instance;
            CategorySeeder.SeedCategories(data);
            PublisherSeeder.SeedPublishers(data);
            AuthorSeeder.SeedAuthors(data);
            BookSeeder.SeedBooks(data);
            var booksService = new BookService(data);

            var actual = booksService.GetBooksCount("", "Fantasy and science fiction");

            Assert.Equal(2, actual);
        }

        [Fact]
        public void GetBooksCountShouldReturnCountOneWhenGivenOnlyCorrectSearchTerm()
        {
            var data = DbMock.Instance;
            CategorySeeder.SeedCategories(data);
            PublisherSeeder.SeedPublishers(data);
            AuthorSeeder.SeedAuthors(data);
            BookSeeder.SeedBooks(data);
            var booksService = new BookService(data);

            var actual = booksService.GetBooksCount("lord", "");

            Assert.Equal(1, actual);
        }

        [Fact]
        public void GetBooksCountShouldReturnAllBooksCountWhenGivenNullInput()
        {
            var data = DbMock.Instance;
            CategorySeeder.SeedCategories(data);
            PublisherSeeder.SeedPublishers(data);
            AuthorSeeder.SeedAuthors(data);
            BookSeeder.SeedBooks(data);
            var booksService = new BookService(data);

            var actual = booksService.GetBooksCount("", "");

            Assert.Equal(8, actual);
        }

        [Fact]
        public void GetBooksCountShouldReturnZeroWhenGivenIncorrectInput()
        {
            var data = DbMock.Instance;
            CategorySeeder.SeedCategories(data);
            PublisherSeeder.SeedPublishers(data);
            AuthorSeeder.SeedAuthors(data);
            BookSeeder.SeedBooks(data);
            var booksService = new BookService(data);

            var actual = booksService.GetBooksCount("invalidSearchTerm", "invalidSelectedCategory");

            Assert.Equal(0, actual);
        }

        [Fact]
        public void DetailsShouldReturnCorrectResultWhenGivenCorrectInput()
        {
            var data = DbMock.Instance;
            CategorySeeder.SeedCategories(data);
            var book = GetBook();
            var booksService = new BookService(data);
            booksService.Create(book.Title, book.Description, book.Price, book.ImageUrl, book.Release, book.Author.Name, book.Publisher.Name, book.CategoryId);
            var expected = new BookServiceModel
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Price = book.Price,
                ImageUrl = book.ImageUrl,
                Release = book.Release,
                AuthorId = book.Author.Id,
                AuthorDescription = book.Author.Description,
                AuthorImage = book.Author.ImageUrl,
                Author = book.Author.Name,
                PublisherId = book.PublisherId,
                Publisher = book.Publisher.Name,
                Category = data.Categories.FirstOrDefault(c => c.Id == book.CategoryId).Name
            };

            var actual = booksService.Details(data.Books.FirstOrDefault().Id);

            Assert.NotNull(actual);
            Assert.Equal(expected.Title, actual.Title);
            Assert.Equal(expected.Description, actual.Description);
            Assert.Equal(expected.Price, actual.Price);
            Assert.Equal(expected.ImageUrl, actual.ImageUrl);
            Assert.Equal(expected.Release, actual.Release);
            Assert.Equal(expected.AuthorDescription, actual.AuthorDescription);
            Assert.Equal(expected.AuthorImage, actual.AuthorImage);
            Assert.Equal(expected.Author, actual.Author);
            Assert.Equal(expected.Publisher, actual.Publisher);
            Assert.Equal(expected.Category, actual.Category);
        }

        [Fact]
        public void DetailsShouldReturnNullWhenGivenIncorrectInput()
        {
            var data = DbMock.Instance;
            CategorySeeder.SeedCategories(data);
            data.Books.Add(GetBook());
            data.SaveChanges();
            var booksService = new BookService(data);

            var result = booksService.Details("incorrectId");

            Assert.Null(result);
        }

        [Fact]
        public void GetBookCategoriesReturnsCorrectResultWhenThereAreCategoriesInTheDb()
        {
            var data = DbMock.Instance;
            CategorySeeder.SeedCategories(data);
            var booksService = new BookService(data);
            var expected = new List<CategoryServiceModel>
            {
                new CategoryServiceModel
                {
                    Name = "Fantasy and science fiction"
                },
                new CategoryServiceModel
                {
                    Name = "Thrillers and horror"
                },
                new CategoryServiceModel
                {
                    Name = "Biography, autobiography, and memoir"
                },
                new CategoryServiceModel
                {
                    Name = "Action and Adventure"
                },
                new CategoryServiceModel
                {
                    Name = "Classics"
                },
                new CategoryServiceModel
                {
                    Name = "Detective and Mystery"
                },
                new CategoryServiceModel
                {
                    Name = "History"
                },
                new CategoryServiceModel
                {
                    Name = "Philosophy"
                },
                new CategoryServiceModel
                {
                    Name = "Arts"
                },
                new CategoryServiceModel
                {
                    Name = "Science"
                },
                new CategoryServiceModel
                {
                    Name = "Children's"
                },
                new CategoryServiceModel
                {
                    Name = "Fiction"
                }
            };

            var actual = booksService.GetBookCategories().ToList();

            Assert.NotEmpty(actual);
            Assert.Equal(expected[0].Name, actual[0].Name);
            Assert.Equal(expected[1].Name, actual[1].Name);
            Assert.Equal(expected[2].Name, actual[2].Name);
            Assert.Equal(expected[3].Name, actual[3].Name);
            Assert.Equal(expected[4].Name, actual[4].Name);
            Assert.Equal(expected[5].Name, actual[5].Name);
            Assert.Equal(expected[6].Name, actual[6].Name);
            Assert.Equal(expected[7].Name, actual[7].Name);
            Assert.Equal(expected[8].Name, actual[8].Name);
            Assert.Equal(expected[9].Name, actual[9].Name);
            Assert.Equal(expected[10].Name, actual[10].Name);
            Assert.Equal(expected[11].Name, actual[11].Name);
        }

        [Fact]
        public void GetBookCategoriesReturnsEmptyCollectionWhenThereAreNoCategoriesInTheDb()
        {
            var data = DbMock.Instance;
            var booksService = new BookService(data);

            var result = booksService.GetBookCategories();

            Assert.Empty(result);
        }

        [Fact]
        public void CreateDoesNotAddAnAlreadyExistingPublisher()
        {
            var data = DbMock.Instance;
            CategorySeeder.SeedCategories(data);
            var book = GetBook();
            var expected = GetPublisher();
            data.Publishers.Add(expected);
            data.SaveChanges();
            var booksService = new BookService(data);

            booksService.Create(book.Title, book.Description, book.Price, book.ImageUrl, book.Release, book.Author.Name, book.Publisher.Name, book.CategoryId);

            Assert.Equal(1, data.Publishers.Count());
            var actual = data.Books.FirstOrDefault().Publisher;

            Assert.Equal(expected.Name, actual.Name);
        }

        [Fact]
        public void CreateDoesNotAddAnAlreadyExistingAuthor()
        {
            var data = DbMock.Instance;
            CategorySeeder.SeedCategories(data);
            var book = GetBook();
            var expected = GetAuthor();
            data.Authors.Add(expected);
            data.SaveChanges();
            var booksService = new BookService(data);

            booksService.Create(book.Title, book.Description, book.Price, book.ImageUrl, book.Release, book.Author.Name, book.Publisher.Name, book.CategoryId);

            Assert.Equal(1, data.Authors.Count());
            var actual = data.Books.FirstOrDefault().Author;

            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Description, actual.Description);
            Assert.Equal(expected.ImageUrl, actual.ImageUrl);
        }

        [Fact]
        public void CreateWorksCorrectlyWhenGivenCorrectInput()
        {
            var data = DbMock.Instance;
            CategorySeeder.SeedCategories(data);
            var book = GetBook();
            var booksService = new BookService(data);

            booksService.Create(book.Title, book.Description, book.Price, book.ImageUrl, book.Release, book.Author.Name, book.Publisher.Name, book.CategoryId);

            Assert.Equal(1, data.Books.Count());

            var actual = data.Books.FirstOrDefault();
            Assert.Equal(book.Title, actual.Title);
            Assert.Equal(book.Description, actual.Description);
            Assert.Equal(book.Price, actual.Price);
            Assert.Equal(book.ImageUrl, actual.ImageUrl);
            Assert.Equal(book.Release, actual.Release);
            Assert.Equal(book.Author.Name, actual.Author.Name);
            Assert.Equal(book.Author.Description, actual.Author.Description);
            Assert.Equal(book.Author.ImageUrl, actual.Author.ImageUrl);
            Assert.Equal(book.Publisher.Name, actual.Publisher.Name);
            Assert.Equal(book.CategoryId, actual.CategoryId);
        }

        [Fact]
        public void CreateDoesNothingWhenSuchABookAlreadyExists()
        {
            var data = DbMock.Instance;
            var book = GetBook();
            data.Books.Add(book);
            data.SaveChanges();
            Assert.Equal(1, data.Books.Count());

            var booksService = new BookService(data);
            booksService.Create(book.Title, book.Description, book.Price, book.ImageUrl, book.Release, book.Author.Name, book.Publisher.Name, book.CategoryId);

            Assert.Equal(1, data.Books.Count());
        }

        [Fact]
        public void CreateDoesNothingWhenGivenIncorrectInput()
        {
            var data = DbMock.Instance;
            var booksService = new BookService(data);

            booksService.Create("invalidTitle", "invalidDescription", 0m, "invalidImageUrl", -5, "invalidAuthorName", "invalidPublisherName", 16);

            Assert.Empty(data.Books);
        }

        private Publisher GetPublisher()
        {
            return new Publisher
            {
                Name = "random publisher"
            };
        }

        private Author GetAuthor()
        {
            return new Author
            {
                Name = "Robert Pantano",
                Description = "random description idk",
                ImageUrl = "nonexistent sorry can't be bothered right now",
            };
        }

        private Book GetBook()
        {
            return new Book
            {
                Title = "Notes from the end of everything",
                Description = "After being diagnosed with a brain tumor, writer John Gallo spends his time confronting his lifelong sense of fraudulence, regret, and self-misunderstanding, all while loosely chronicling the development of his cancer.",
                Price = 10m,
                ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/417Y0vAzvML._SY291_BO1,204,203,200_QL40_FMwebp_.jpg",
                Release = 2020,
                AuthorId = Guid.NewGuid().ToString(),
                Author = new Author
                {
                    Name = "Robert Pantano",
                    Description = "Robert Pantano is the creator of the YouTube channel and production house known as Pursuit of Wonder, which covers similar topics of philosophy, science, and literature through short stories, guided experiences, video essays, and more.",
                    ImageUrl = "https://yt3.ggpht.com/ytc/AKedOLSOgx4aSr0YiJreg-4ReQwO4hKw_wbVSKcrIf5JCQ=s900-c-k-c0x00ffffff-no-rj"
                },
                PublisherId = Guid.NewGuid().ToString(),
                Publisher = new Publisher
                {
                    Name = "random publisher"
                },
                CategoryId = 3,
            };
        }
    }
}

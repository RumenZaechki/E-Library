using E_Library.Data.Models;
using E_Library.Services.Authors;
using E_Library.Services.Authors.Models;
using E_Library.Services.Contracts;
using E_Library.Test.Mocks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace E_Library.Test.Services
{
    public class AuthorServiceTests
    {
        [Fact]
        public async Task EditWorksCorrectlyWhenGivenCorrectInput()
        {
            var data = DbMock.Instance;
            var author = new Author
            {
                Name = "Herman Melville",
                Description = "random description idk",
                ImageUrl = "nonexistent sorry can't be bothered right now"
            };
            var expected = new Author
            {
                Name = "Random Dude",
                Description = "Another random description",
                ImageUrl = "When I said that I can't be bothered, I meant that I can't be bothered, ok?"
            };
            data.Authors.Add(author);
            data.SaveChanges();
            IAuthorService authorService = new AuthorService(data);

            await authorService.EditAsync(author.Id, "Random Dude", "Another random description", "When I said that I can't be bothered, I meant that I can't be bothered, ok?");
            var actual = await authorService.GetAuthorAsync(author.Id);

            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Description, actual.Description);
            Assert.Equal(expected.ImageUrl, actual.ImageUrl);
        }

        [Fact]
        public async Task EditShouldDoNothingWhenGivenIncorrectInput()
        {
            var data = DbMock.Instance;
            var expected = new Author
            {
                Name = "Herman Melville",
                Description = "random description idk",
                ImageUrl = "nonexistent sorry can't be bothered right now",
            };
            data.Authors.Add(expected);
            data.SaveChanges();
            IAuthorService authorService = new AuthorService(data);

            await authorService.EditAsync("invalidId", "", "", "");
            var actual = await authorService.GetAuthorAsync(expected.Id);

            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Description, actual.Description);
            Assert.Equal(expected.ImageUrl, actual.ImageUrl);
        }

        [Fact]
        public async Task GetAuthorsShouldReturnCorrectResultWhenGivenCorrectInputAndSearchTerm()
        {
            var data = DbMock.Instance;
            data.Authors.AddRange(GetAuthors());
            data.SaveChanges();
            IAuthorService authorService = new AuthorService(data);
            var expected = new List<AuthorServiceModel>()
            {
                new AuthorServiceModel
                {
                    Name = "Herman Melville",
                    Description = "random description idk",
                    ImageUrl = "nonexistent sorry can't be bothered right now"
                }
            };

            var actual = await authorService
                    .GetAuthorsAsync(-1, -2, "hE");
            var res = actual
                    .OrderByDescending(a => a.Name)
                    .ToList();

            Assert.Equal(2, data.Authors.Count());
            Assert.Equal(expected[0].Name, res[0].Name);
        }

        [Fact]
        public async Task GetAuthorsShouldReturnCorrectResultWhenGivenIncorrectInputAndNoSearchTerm()
        {
            var data = DbMock.Instance;
            data.Authors.AddRange(GetAuthors());
            data.SaveChanges();
            IAuthorService authorService = new AuthorService(data);
            var expected = new List<AuthorServiceModel>()
            {
                new AuthorServiceModel
                {
                    Name = "Herman Melville",
                    Description = "random description idk",
                    ImageUrl = "nonexistent sorry can't be bothered right now"
                },
                new AuthorServiceModel
                {
                    Name = "Flea",
                    Description = "random description idk",
                    ImageUrl = "nonexistent sorry can't be bothered right now"
                }
            };

            var actual = await authorService
                    .GetAuthorsAsync(-1, -2, "");
            var res = actual
                    .OrderByDescending(a => a.Name)
                    .ToList();

            Assert.Equal(2, data.Authors.Count());
            Assert.Equal(expected[0].Name, res[0].Name);
            Assert.Equal(expected[1].Name, res[1].Name);
        }

        [Fact]
        public async Task GetAuthorsCountShouldReturnCorrectResultWhenGivenASearchTerm()
        {
            var data = DbMock.Instance;
            data.Authors.AddRange(GetAuthors());
            data.SaveChanges();
            IAuthorService authorService = new AuthorService(data);

            var result = await authorService.GetAuthorsCountAsync("Herman");

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task GetAuthorsCountShouldReturnCorrectResultWhenGivenNoSearchTerm()
        {
            var data = DbMock.Instance;
            data.Authors.AddRange(GetAuthors());
            data.SaveChanges();
            IAuthorService authorService = new AuthorService(data);

            var result = await authorService.GetAuthorsCountAsync("");

            Assert.Equal(2, result);
        }

        [Fact]
        public async Task AddWorksCorrectlyWhenGivenCorrectInput()
        {
            var data = DbMock.Instance;
            IAuthorService authorService = new AuthorService(data);

            await authorService.AddAsync("Herman Melville", "random description idk", "nonexistent sorry can't be bothered right now");
            var result = data.Authors.FirstOrDefault();

            Assert.NotEmpty(data.Authors);
            Assert.Equal(1, data.Authors.Count());
            Assert.Equal("Herman Melville", result.Name);
        }

        [Fact]
        public async Task AddShouldDoNothingWhenAuthorAlreadyExists()
        {
            var data = DbMock.Instance;
            data.Authors.AddRange(GetAuthors());
            data.SaveChanges();
            IAuthorService authorService = new AuthorService(data);

            await authorService.AddAsync("Herman Melville", "random description idk", "nonexistent sorry can't be bothered right now");

            Assert.Equal(2, data.Authors.Count());
            Assert.Equal(1, data.Authors.Where(a => a.Name == "Herman Melville").Count());
        }

        [Fact]
        public async Task AddShouldDoNothingWhenGivenIncorrectInput()
        {
            var data = DbMock.Instance;
            IAuthorService authorService = new AuthorService(data);

            await authorService.AddAsync("", "", "");

            Assert.Empty(data.Authors);
        }

        [Fact]
        public async Task GetAuthorShouldReturnCorrectAuthorWhenGivenCorrectInput()
        {
            var data = DbMock.Instance;
            data.Authors.AddRange(GetAuthors());
            data.SaveChanges();
            IAuthorService authorService = new AuthorService(data);
            var actual = await authorService.GetAuthorAsync("1");
            var expected = new AuthorServiceModel
            {
                Name = "Herman Melville",
                Description = "random description idk",
                ImageUrl = "nonexistent sorry can't be bothered right now"
            };
            Assert.NotNull(actual);
            Assert.Equal(expected.Name, actual.Name);
            Assert.Equal(expected.Description, actual.Description);
            Assert.Equal(expected.ImageUrl, actual.ImageUrl);
        }

        [Fact]
        public async Task GetAuthorShouldReturnNullIfGivenInvalidInput()
        {
            var data = DbMock.Instance;
            data.Authors.AddRange(GetAuthors());
            data.SaveChanges();
            IAuthorService authorService = new AuthorService(data);

            var actual = await authorService.GetAuthorAsync("invalid");

            Assert.Null(actual);
        }

        private Author[] GetAuthors()
        {
            return new Author[]
            {
                new Author
                {
                    Id = "1",
                    Name = "Herman Melville",
                    Description = "random description idk",
                    ImageUrl = "nonexistent sorry can't be bothered right now"
                },
                new Author
                {
                    Id="2",
                    Name = "Flea",
                    Description = "random description idk",
                    ImageUrl = "nonexistent sorry can't be bothered right now"
                }
            };
        }
    }
}

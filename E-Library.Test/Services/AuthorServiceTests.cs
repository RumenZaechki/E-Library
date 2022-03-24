using E_Library.Data;
using E_Library.Data.Models;
using E_Library.Services.Authors;
using E_Library.Services.Authors.Models;
using E_Library.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace E_Library.Test.Services
{
    public class AuthorServiceTests
    {
        private readonly LibraryDbContext data;
        public AuthorServiceTests()
        {
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            this.data = new LibraryDbContext(options);
            this.data.Authors.AddRange(new[]
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
            });
            this.data.SaveChanges();
        }
        [Fact]
        public void GetAuthorShouldReturnCorrectAuthorWhenGivenCorrectInput()
        {
            IAuthorService authorService = new AuthorService(this.data);
            var actual = authorService.GetAuthor("1");
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
        public void GetAuthorShouldReturnNullIfGivenNullInput()
        {
            IAuthorService authorService = new AuthorService(this.data);
            var actual = authorService.GetAuthor(null);
            Assert.Null(actual);
        }
    }
}

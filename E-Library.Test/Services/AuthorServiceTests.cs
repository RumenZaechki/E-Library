using E_Library.Data;
using E_Library.Data.Models;
using E_Library.Services.Authors;
using E_Library.Services.Authors.Models;
using E_Library.Services.Contracts;
using E_Library.Test.Mocks;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace E_Library.Test.Services
{
    public class AuthorServiceTests
    {
        [Fact]
        public void GetAuthorShouldReturnCorrectAuthorWhenGivenCorrectInput()
        {
            var data = DbMock.Instance;
            data.Authors.AddRange(GetAuthors());
            data.SaveChanges();
            IAuthorService authorService = new AuthorService(data);
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
            var data = DbMock.Instance;
            data.Authors.AddRange(GetAuthors());
            data.SaveChanges();
            IAuthorService authorService = new AuthorService(data);
            var actual = authorService.GetAuthor(null);
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

using E_Library.Data.Models;
using E_Library.Services.Carts;
using E_Library.Test.Mocks;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace E_Library.Test.Services
{
    public class CartServiceTests
    {
        [Fact]
        public async Task RemoveBookFromCartShouldWorkCorrectlyWhenGivenCorrectInput()
        {
            var data = DbMock.Instance;
            var user = GetUser();
            var book = GetBook();
            data.Users.Add(user);
            data.Books.Add(book);
            data.SaveChanges();
            var cartService = new CartService(data);

            await cartService.AddBookToCartAsync(user.Id, book.Id);

            Assert.NotEmpty(user.Cart.CartBooks);
            Assert.Equal(1, user.Cart.CartBooks.Count);

            await cartService.RemoveBookFromCartAsync(book.Id, user.Id);

            Assert.Empty(user.Cart.CartBooks);
        }

        [Fact]
        public async Task RemoveBookFromCartShouldDoNothingWhenGivenIncorrectInput()
        {
            var data = DbMock.Instance;
            var user = GetUser();
            var book = GetBook();
            data.Users.Add(user);
            data.Books.Add(book);
            data.SaveChanges();
            var cartService = new CartService(data);

            await cartService.AddBookToCartAsync(user.Id, book.Id);
            await cartService.RemoveBookFromCartAsync("incorrectId", "anotherIncorrectId");

            Assert.NotEmpty(user.Cart.CartBooks);
            Assert.Equal(1, user.Cart.CartBooks.Count);

            await cartService.RemoveBookFromCartAsync(book.Id, "anotherIncorrectId");

            Assert.NotEmpty(user.Cart.CartBooks);
            Assert.Equal(1, user.Cart.CartBooks.Count);

            await cartService.RemoveBookFromCartAsync("incorrectId", user.Id);

            Assert.NotEmpty(user.Cart.CartBooks);
            Assert.Equal(1, user.Cart.CartBooks.Count);
        }

        [Fact]
        public async Task BuyShouldWorkCorrectlyWhenGivenCorrectInput()
        {
            var data = DbMock.Instance;
            var user = GetUser();
            var book = GetBook();
            data.Users.Add(user);
            data.Books.Add(book);
            data.SaveChanges();
            var cartService = new CartService(data);

            await cartService.AddBookToCartAsync(user.Id, book.Id);

            Assert.NotEmpty(user.Cart.CartBooks);

            await cartService.BuyAsync(user.Id);

            Assert.Empty(user.Cart.CartBooks);
        }

        [Fact]
        public async Task BuyShouldDoNothingWhenGivenIncorrectInput()
        {
            var data = DbMock.Instance;
            var user = GetUser();
            var book = GetBook();
            data.Users.Add(user);
            data.Books.Add(book);
            data.SaveChanges();
            var cartService = new CartService(data);
            await cartService.AddBookToCartAsync(user.Id, book.Id);

            await cartService.BuyAsync("incorrectId");

            Assert.NotEmpty(user.Cart.CartBooks);
        }

        [Fact]
        public async Task GetBooksFromCartShouldReturnCorrectResultWhenThereAreBooksInCart()
        {
            var data = DbMock.Instance;
            var user = GetUser();
            var book = GetBook();
            data.Users.Add(user);
            data.Books.Add(book);
            data.SaveChanges();
            var cartService = new CartService(data);
            var expected = new CartBookModel
            {
                Id = book.Id,
                Title = book.Title,
                Price = book.Price.ToString("F2")
            };

            await cartService.AddBookToCartAsync(user.Id, book.Id);
            var result = await cartService.GetBooksFromCartAsync(user.Id);
            var actual = result.FirstOrDefault();

            Assert.NotEmpty(result);
            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Title, actual.Title);
            Assert.Equal(expected.Price, actual.Price);
        }

        [Fact]
        public async Task GetBooksFromCartShouldReturnEmptyCollectionWhenNoBooksAreInCart()
        {
            var data = DbMock.Instance;
            var user = GetUser();
            var book = GetBook();
            data.Users.Add(user);
            data.Books.Add(book);
            data.SaveChanges();
            var cartService = new CartService(data);

            var result = await cartService.GetBooksFromCartAsync(user.Id);

            Assert.Empty(result);
        }

        [Fact]
        public async Task IsBookInCartShouldReturnTrueWhenGivenBookThatIsInCart()
        {
            var data = DbMock.Instance;
            var user = GetUser();
            var book = GetBook();
            data.Users.Add(user);
            data.Books.Add(book);
            data.SaveChanges();
            var cartService = new CartService(data);

            await cartService.AddBookToCartAsync(user.Id, book.Id);
            var result = await cartService.IsBookInCartAsync(user.Id, book.Id);

            Assert.True(result);
        }

        [Fact]
        public async Task IsBookInCartShouldReturnFalseWhenGivenBookThatIsNotInCart()
        {
            var data = DbMock.Instance;
            var user = GetUser();
            var secondUser = GetUser();
            var book = GetBook();
            data.Users.Add(user);
            data.Users.Add(secondUser);
            data.Books.Add(book);
            data.SaveChanges();
            var cartService = new CartService(data);

            await cartService.AddBookToCartAsync(user.Id, book.Id);
            var result = await cartService.IsBookInCartAsync(secondUser.Id, book.Id);

            Assert.False(result);
        }

        [Fact]
        public async Task IsBookInCartShouldReturnFalseWhenGivenAnEmptyCart()
        {
            var data = DbMock.Instance;
            var user = GetUser();
            var book = GetBook();
            data.Users.Add(user);
            data.Books.Add(book);
            data.SaveChanges();
            var cartService = new CartService(data);

            var result = await cartService.IsBookInCartAsync(user.Id, book.Id);

            Assert.False(result);
        }

        [Fact]
        public async Task AddToCartWorksCorrectlyWhenGivenCorrectInput()
        {
            var data = DbMock.Instance;
            var user = GetUser();
            var book = GetBook();
            data.Users.Add(user);
            data.Books.Add(book);
            data.SaveChanges();
            var cartService = new CartService(data);

            await cartService.AddBookToCartAsync(user.Id, book.Id);

            var result = data.BookCarts.FirstOrDefault(bc => bc.CartId == user.Cart.Id && bc.BookId == book.Id);
            Assert.NotNull(result);
            Assert.Equal(book.Id, result.Book.Id);
            Assert.Equal(user.Cart.Id, result.Cart.Id);
        }

        [Fact]
        public async Task AddToCartShouldDoNothingWhenGivenIncorrectInput()
        {
            var data = DbMock.Instance;
            var cartService = new CartService(data);

            await cartService.AddBookToCartAsync("incorrectId", "anotherIncorrectId");

            Assert.Empty(data.BookCarts);
        }

        private User GetUser()
        {
            return new User
            {
                Cart = new Cart()
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
                    Name = "Independently published"
                },
                CategoryId = 3,
            };
        }
    }
}

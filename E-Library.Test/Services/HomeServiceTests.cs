﻿using E_Library.Data.Models;
using E_Library.Services.Books.Models;
using E_Library.Services.Contracts;
using E_Library.Services.Home;
using E_Library.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace E_Library.Test.Services
{
    public class HomeServiceTests
    {
        [Fact]
        public async Task GetBooksShouldReturnTheThreeBooksAddedAtTheLatest()
        {
            var data = DbMock.Instance;
            data.Books.AddRange(GetBooks());
            data.SaveChanges();
            IHomeService homeService = new HomeService(data);

            var actual = await homeService.GetRecentBooksAsync();
            var expected = GetModels();

            Assert.NotNull(actual);
            Assert.Equal(4, data.Books.Count());
            Assert.Equal(3, actual.Count());
            Assert.Equal(expected[0].Title, actual[0].Title);
            Assert.Equal(expected[1].Title, actual[1].Title);
            Assert.Equal(expected[2].Title, actual[2].Title);
        }

        [Fact]
        public async Task GetBooksShouldReturnEmptyListWhenDbIsEmpty()
        {
            var data = DbMock.Instance;
            IHomeService homeService = new HomeService(data);

            var actual = await homeService.GetRecentBooksAsync();

            Assert.Empty(actual);
        }

        private List<BookServiceModel> GetModels()
        {
            return new List<BookServiceModel>(new[]
            {
                new BookServiceModel
                {
                    Title ="The Art of War"
                },
                new BookServiceModel
                {
                    Title ="Meditations"
                },
                new BookServiceModel
                {
                    Title ="Lord Of The Rings"
                }
            });
        }

        private Book[] GetBooks()
        {
            return new[]
            {
                new Book
                {
                    Title ="Notes from the end of everything",
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
                },
                new Book
                {
                    Title ="Lord Of The Rings",
                    Description = "The Lord of the Rings is an epic high-fantasy novel by English author and scholar J. R. R. Tolkien. Set in Middle-earth, intended to be Earth at some distant time in the past, the story began as a sequel to Tolkien's 1937 children's book The Hobbit, but eventually developed into a much larger work.",
                    Price = 40m,
                    ImageUrl = "https://m.media-amazon.com/images/P/0618640150.01._SCLZZZZZZZ_SX500_.jpg",
                    Release = 1954,
                    AuthorId = Guid.NewGuid().ToString(),
                    Author = new Author
                    {
                        Name = "John Tolkien",
                        Description = "John Ronald Reuel Tolkien was an English writer, poet, philologist, and academic, best known as the author of the high fantasy works The Hobbit and The Lord of the Rings. From 1925-45, Tolkien was the Rawlinson and Bosworth Professor of Anglo-Saxon and a Fellow of Pembroke College, both at the University of Oxford. He then moved within the same university, to become the Merton Professor of English Language and Literature and Fellow of Merton College, positions he held from 1945 until his retirement in 1959. Tolkien was a close friend of C. S. Lewis, a co-member of the informal literary discussion group The Inklings. He was appointed a Commander of the Order of the British Empire by Queen Elizabeth II on 28 March 1972.",
                        ImageUrl = "https://m.media-amazon.com/images/M/MV5BMGMxMmRkNzctMWQzYy00MTY3LWEzMDAtMzEzMDhkZWI4MjZlXkEyXkFqcGdeQXVyNDUzOTQ5MjY@._V1_.jpg"
                    },
                    PublisherId = Guid.NewGuid().ToString(),
                    Publisher = new Publisher
                    {
                        Name = "William Morrow Paperbacks"
                    },
                    CategoryId = 1,
                },
                new Book
                {
                    Title ="Meditations",
                    Description = "Meditations is a series of personal writings by Marcus Aurelius, Roman Emperor 161–180 CE, setting forth his ideas on Stoic philosophy.Marcus Aurelius wrote the 12 books of the Meditations in Koine Greek as a source for his own guidance and self-improvement.",
                    Price = 15m,
                    ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/51cQEdN9KuL._SX331_BO1,204,203,200_.jpg",
                    Release = 2018,
                    AuthorId = Guid.NewGuid().ToString(),
                    Author = new Author
                    {
                        Name = "Marcus Aurelius",
                        Description = "Marcus Aurelius Antoninus was Roman emperor from 161 to 180 and a Stoic philosopher. He was the last of the rulers known as the Five Good Emperors, and the last emperor of the Pax Romana, an age of relative peace and stability for the Roman Empire lasting from 27 BCE to 180 CE.",
                        ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT9OsS0Bl4dOwm0DFcR5U0H8neR_fyJ9J_ePQ&usqp=CAU"
                    },
                    PublisherId = Guid.NewGuid().ToString(),
                    Publisher = new Publisher
                    {
                        Name = "CreateSpace Independent Publishing Platform"
                    },
                    CategoryId = 8,
                },
                new Book
                {
                    Title ="The Art of War",
                    Description = "Sun Tzu is thought to have been a military general and adviser to the king of the Chinese state of Wu during the sixth century BCE. Although some modern scholars have called his authorship into doubt, the world's most influential treatise on military strategy, The Art of War, bears his name.",
                    Price = 11m,
                    ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/4145Q3WAneL._SX331_BO1,204,203,200_.jpg",
                    Release = 2000,
                    AuthorId = Guid.NewGuid().ToString(),
                    Author = new Author
                    {
                        Name = "Sun Tzu",
                        Description = "Sun Tzu was a Chinese general, military strategist, writer, and philosopher who lived in the Eastern Zhou period of ancient China. Sun Tzu is traditionally credited as the author of The Art of War, an influential work of military strategy that has affected both Western and East Asian philosophy and military thinking.",
                        ImageUrl = "https://game-change.com/wp-content/uploads/2021/08/Sun-Tzu-3.jpg"
                    },
                    PublisherId = Guid.NewGuid().ToString(),
                    Publisher = new Publisher
                    {
                        Name = "Filiquarian"
                    },
                    CategoryId = 7,
                }
            };
        }
    }
}
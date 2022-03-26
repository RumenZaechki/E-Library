using E_Library.Data;
using E_Library.Data.Models;

namespace E_Library.Infrastructure
{
    public static class AuthorSeeder
    {
        public static void SeedAuthors(LibraryDbContext data)
        {
            if (data.Authors.Any())
            {
                return;
            }
            data.Authors.AddRange(new[]
            {
                new Author
                {
                    Name = "Robert Pantano",
                    Description = "Robert Pantano is the creator of the YouTube channel and production house known as Pursuit of Wonder, which covers similar topics of philosophy, science, and literature through short stories, guided experiences, video essays, and more.",
                    ImageUrl = "https://yt3.ggpht.com/ytc/AKedOLSOgx4aSr0YiJreg-4ReQwO4hKw_wbVSKcrIf5JCQ=s900-c-k-c0x00ffffff-no-rj",
                    PublisherId = data.Publishers.FirstOrDefault(p => p.Name == "Independently Published").Id,
                    Books = data.Books.Where(p => p.Author.Name == "Robert Pantano").ToList()
                },
                new Author
                {
                    Name = "John Tolkien",
                    Description = "John Ronald Reuel Tolkien was an English writer, poet, philologist, and academic, best known as the author of the high fantasy works The Hobbit and The Lord of the Rings. From 1925-45, Tolkien was the Rawlinson and Bosworth Professor of Anglo-Saxon and a Fellow of Pembroke College, both at the University of Oxford. He then moved within the same university, to become the Merton Professor of English Language and Literature and Fellow of Merton College, positions he held from 1945 until his retirement in 1959. Tolkien was a close friend of C. S. Lewis, a co-member of the informal literary discussion group The Inklings. He was appointed a Commander of the Order of the British Empire by Queen Elizabeth II on 28 March 1972.",
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BMGMxMmRkNzctMWQzYy00MTY3LWEzMDAtMzEzMDhkZWI4MjZlXkEyXkFqcGdeQXVyNDUzOTQ5MjY@._V1_.jpg",
                    PublisherId = data.Publishers.FirstOrDefault(p => p.Name == "William Morrow Paperbacks").Id,
                    Books = data.Books.Where(p => p.Author.Name == "John Tolkien").ToList()
                },
                new Author
                {
                    Name = "Marcus Aurelius",
                    Description = "Marcus Aurelius Antoninus was Roman emperor from 161 to 180 and a Stoic philosopher. He was the last of the rulers known as the Five Good Emperors, and the last emperor of the Pax Romana, an age of relative peace and stability for the Roman Empire lasting from 27 BCE to 180 CE.",
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT9OsS0Bl4dOwm0DFcR5U0H8neR_fyJ9J_ePQ&usqp=CAU",
                    PublisherId = data.Publishers.FirstOrDefault(p => p.Name == "CreateSpace Independent Publishing Platform").Id,
                    Books = data.Books.Where(p => p.Author.Name == "Marcus Aurelius").ToList()
                },
                new Author
                {
                    Name = "Sun Tzu",
                    Description = "Sun Tzu was a Chinese general, military strategist, writer, and philosopher who lived in the Eastern Zhou period of ancient China. Sun Tzu is traditionally credited as the author of The Art of War, an influential work of military strategy that has affected both Western and East Asian philosophy and military thinking.",
                    ImageUrl = "https://game-change.com/wp-content/uploads/2021/08/Sun-Tzu-3.jpg",
                    PublisherId = data.Publishers.FirstOrDefault(p => p.Name == "Filiquarian").Id,
                    Books = data.Books.Where(p => p.Author.Name == "Sun Tzu").ToList()
                },
                new Author
                {
                    Name = "Yuval Harari",
                    Description = "Prof. Yuval Noah Harari has a PhD in History from the University of Oxford and lectures at the Hebrew University of Jerusalem, specializing in world history. His books have been translated into 65 languages, with over 35 million copies sold worldwide.",
                    ImageUrl = "https://m.media-amazon.com/images/S/amzn-author-media-prod/jorjp12f9jbo3ojjmn1aacli7s._SX450_.jpg",
                    PublisherId = data.Publishers.FirstOrDefault(p => p.Name == "Dvir Publishing House Ltd.").Id,
                    Books = data.Books.Where(p => p.Author.Name == "Yuval Harari").ToList()
                },
                new Author
                {
                    Name = "Aldous Huxley",
                    Description = "Aldous Huxley (1894-1963) is the author of the classic novels Island, Eyeless in Gaza, and The Genius and the Goddess, as well as such critically acclaimed nonfiction works as The Devils of Loudun, The Doors of Perception, and The Perennial Philosophy. Born in Surrey, England, and educated at Oxford, he died in Los Angeles.",
                    ImageUrl = "https://m.media-amazon.com/images/I/316pVALJwjL._SX450_.jpg",
                    PublisherId = data.Publishers.FirstOrDefault(p => p.Name == "Harper Perennial").Id,
                    Books = data.Books.Where(p => p.Author.Name == "Aldous Huxley").ToList()
                },
                new Author
                {
                    Name = "Douglas Adams",
                    Description = "Douglas Adams (1952-2001) was the much-loved author of the Hitchhiker's Guides, all of which have sold more than 15 million copies worldwide.",
                    ImageUrl = "https://m.media-amazon.com/images/I/41DmXuq2yaL._SX450_.jpg",
                    PublisherId = data.Publishers.FirstOrDefault(p => p.Name == "Del Rey").Id,
                    Books = data.Books.Where(p => p.Author.Name == "Douglas Adams").ToList()
                },
                new Author
                {
                    Name = "Stephen King",
                    Description = "Stephen King is the author of more than fifty books, all of them worldwide bestsellers. His first crime thriller featuring Bill Hodges, MR MERCEDES, won the Edgar Award for best novel and was shortlisted for the CWA Gold Dagger Award.",
                    ImageUrl = "https://m.media-amazon.com/images/S/amzn-author-media-prod/fkeglaqq0pic05a0v6ieqt4iv5._SX450_.jpg",
                    PublisherId = data.Publishers.FirstOrDefault(p => p.Name == "Simon & Schuster").Id,
                    Books = data.Books.Where(p => p.Author.Name == "Stephen King").ToList()
                },
            });
            data.SaveChanges();
        }
    }
}

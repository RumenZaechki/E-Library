using E_Library.Data;
using E_Library.Data.Models;

namespace E_Library.Infrastructure
{
    public static class BookSeeder
    {
        public static void SeedBooks(LibraryDbContext data)
        {
            if (data.Books.Any())
            {
                return;
            }
            data.Books.AddRange(new[]
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
                },
                new Book
                {
                    Title ="Sapiens: A Brief History of Humankind",
                    Description = "From a renowned historian comes a groundbreaking narrative of humanity’s creation and evolution—a #1 international bestseller—that explores the ways in which biology and history have defined us and enhanced our understanding of what it means to be “human.” One hundred thousand years ago, at least six different species of humans inhabited Earth. Yet today there is only one—homo sapiens. What happened to the others? And what may happen to us?",
                    Price = 24.18m,
                    ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/41+lolL22gL._SX314_BO1,204,203,200_.jpg",
                    Release = 2015,
                    AuthorId = Guid.NewGuid().ToString(),
                    Author = new Author
                    {
                        Name = "Yuval Harari",
                        Description = "Prof. Yuval Noah Harari has a PhD in History from the University of Oxford and lectures at the Hebrew University of Jerusalem, specializing in world history. His books have been translated into 65 languages, with over 35 million copies sold worldwide.",
                        ImageUrl = "https://m.media-amazon.com/images/S/amzn-author-media-prod/jorjp12f9jbo3ojjmn1aacli7s._SX450_.jpg"
                    },
                    PublisherId = Guid.NewGuid().ToString(),
                    Publisher = new Publisher
                    {
                        Name = "Dvir Publishing House Ltd."
                    },
                    CategoryId = 10,
                },
                new Book
                {
                    Title ="Brave New World",
                    Description = "Aldous Huxley's profoundly important classic of world literature, Brave New World is a searching vision of an unequal, technologically-advanced future where humans are genetically bred, socially indoctrinated, and pharmaceutically anesthetized to passively uphold an authoritarian ruling order–all at the cost of our freedom, full humanity, and perhaps also our souls. “A genius [who] who spent his life decrying the onward march of the Machine” (The New Yorker), Huxley was a man of incomparable talents: equally an artist, a spiritual seeker, and one of history’s keenest observers of human nature and civilization. Brave New World, his masterpiece, has enthralled and terrified millions of readers, and retains its urgent relevance to this day as both a warning to be heeded as we head into tomorrow and as thought-provoking, satisfying work of literature. ",
                    Price = 13.99m,
                    ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/41-n-3hZMeL._SX325_BO1,204,203,200_.jpg",
                    Release = 2006,
                    AuthorId = Guid.NewGuid().ToString(),
                    Author = new Author
                    {
                        Name = "Aldous Huxley",
                        Description = "Aldous Huxley (1894-1963) is the author of the classic novels Island, Eyeless in Gaza, and The Genius and the Goddess, as well as such critically acclaimed nonfiction works as The Devils of Loudun, The Doors of Perception, and The Perennial Philosophy. Born in Surrey, England, and educated at Oxford, he died in Los Angeles.",
                        ImageUrl = "https://m.media-amazon.com/images/I/316pVALJwjL._SX450_.jpg"
                    },
                    PublisherId = Guid.NewGuid().ToString(),
                    Publisher = new Publisher
                    {
                        Name = "Harper Perennial"
                    },
                    CategoryId = 12,
                },
                new Book
                {
                    Title ="The Hitchhiker's Guide to the Galaxy",
                    Description = "It’s an ordinary Thursday morning for Arthur Dent . . . until his house gets demolished. The Earth follows shortly after to make way for a new hyperspace express route, and Arthur’s best friend has just announced that he’s an alien. After that, things get much, much worse. With just a towel, a small yellow fish, and a book, Arthur has to navigate through a very hostile universe in the company of a gang of unreliable aliens. Luckily the fish is quite good at languages. And the book is The Hitchhiker’s Guide to the Galaxy . . . which helpfully has the words DON’T PANIC inscribed in large, friendly letters on its cover.",
                    Price = 7.03m,
                    ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/51MzUz8rQcL._SX305_BO1,204,203,200_.jpg",
                    Release = 1995,
                    AuthorId = Guid.NewGuid().ToString(),
                    Author = new Author
                    {
                        Name = "Douglas Adams",
                        Description = "Douglas Adams (1952-2001) was the much-loved author of the Hitchhiker's Guides, all of which have sold more than 15 million copies worldwide.",
                        ImageUrl = "https://m.media-amazon.com/images/I/41DmXuq2yaL._SX450_.jpg"
                    },
                    PublisherId = Guid.NewGuid().ToString(),
                    Publisher = new Publisher
                    {
                        Name = "Del Rey"
                    },
                    CategoryId = 1,
                },
                new Book
                {
                    Title ="Cell",
                    Description = "On October 1, God is in His heaven, the stock market stands at 10,140, most of the planes are on time, and graphic artist Clayton Riddell is visiting Boston, having just landed a deal that might finally enable him to make art instead of teaching it. But all those good feelings about the future change in a hurry thanks to a devastating phenomenon that will come to be known as The Pulse. The delivery method is a cell phone—everyone’s cell phone. Now Clay and the few survivors who join him find themselves in the pitch-black night of civilization’s darkest age, surrounded by chaos, carnage, and a relentless human horde that has been reduced to its basest nature...and then begins to evolve. There’s no escaping this nightmare. But for Clay, an arrow points the way home to his family in Maine, and as he and his fellow refugees make their journey north, they begin to see the crude signs confirming their direction. A promise of a safe haven, or quite possibly the deadliest trap of all....",
                    Price = 11.83m,
                    ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/51GfoUqzhKL._SX273_BO1,204,203,200_.jpg",
                    Release = 2016,
                    AuthorId = Guid.NewGuid().ToString(),
                    Author = new Author
                    {
                        Name = "Stephen King",
                        Description = "Stephen King is the author of more than fifty books, all of them worldwide bestsellers. His first crime thriller featuring Bill Hodges, MR MERCEDES, won the Edgar Award for best novel and was shortlisted for the CWA Gold Dagger Award.",
                        ImageUrl = "https://m.media-amazon.com/images/S/amzn-author-media-prod/fkeglaqq0pic05a0v6ieqt4iv5._SX450_.jpg"
                    },
                    PublisherId = Guid.NewGuid().ToString(),
                    Publisher = new Publisher
                    {
                        Name = "Simon & Schuster"
                    },
                    CategoryId = 2,
                }
            });
            data.SaveChanges();
        }
    }
}

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
                    AuthorId = data.Authors.FirstOrDefault(a => a.Name == "Robert Pantano").Id,
                    Author = data.Authors.FirstOrDefault(a => a.Name == "Robert Pantano"),
                    PublisherId = data.Publishers.FirstOrDefault(p => p.Name == "Independently published").Id,
                    Publisher = data.Publishers.FirstOrDefault(p => p.Name == "Independently published"),
                    CategoryId = 3,
                },
                new Book
                {
                    Title ="Lord Of The Rings",
                    Description = "The Lord of the Rings is an epic high-fantasy novel by English author and scholar J. R. R. Tolkien. Set in Middle-earth, intended to be Earth at some distant time in the past, the story began as a sequel to Tolkien's 1937 children's book The Hobbit, but eventually developed into a much larger work.",
                    Price = 40m,
                    ImageUrl = "https://m.media-amazon.com/images/P/0618640150.01._SCLZZZZZZZ_SX500_.jpg",
                    Release = 1954,
                    AuthorId = data.Authors.FirstOrDefault(a => a.Name == "John Tolkien").Id,
                    Author = data.Authors.FirstOrDefault(a => a.Name == "John Tolkien"),
                    PublisherId = data.Publishers.FirstOrDefault(p => p.Name == "William Morrow Paperbacks").Id,
                    Publisher = data.Publishers.FirstOrDefault(p => p.Name == "William Morrow Paperbacks"),
                    CategoryId = 1,
                },
                new Book
                {
                    Title ="Meditations",
                    Description = "Meditations is a series of personal writings by Marcus Aurelius, Roman Emperor 161–180 CE, setting forth his ideas on Stoic philosophy.Marcus Aurelius wrote the 12 books of the Meditations in Koine Greek as a source for his own guidance and self-improvement.",
                    Price = 15m,
                    ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/51cQEdN9KuL._SX331_BO1,204,203,200_.jpg",
                    Release = 2018,
                    AuthorId = data.Authors.FirstOrDefault(a => a.Name == "Marcus Aurelius").Id,
                    Author = data.Authors.FirstOrDefault(a => a.Name == "Marcus Aurelius"),
                    PublisherId = data.Publishers.FirstOrDefault(p => p.Name == "CreateSpace Independent Publishing Platform").Id,
                    Publisher = data.Publishers.FirstOrDefault(p => p.Name == "CreateSpace Independent Publishing Platform"),
                    CategoryId = 8,
                },
                new Book
                {
                    Title ="The Art of War",
                    Description = "Sun Tzu is thought to have been a military general and adviser to the king of the Chinese state of Wu during the sixth century BCE. Although some modern scholars have called his authorship into doubt, the world's most influential treatise on military strategy, The Art of War, bears his name.",
                    Price = 11m,
                    ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/4145Q3WAneL._SX331_BO1,204,203,200_.jpg",
                    Release = 2000,
                    AuthorId = data.Authors.FirstOrDefault(a => a.Name == "Sun Tzu").Id,
                    Author = data.Authors.FirstOrDefault(a => a.Name == "Sun Tzu"),
                    PublisherId = data.Publishers.FirstOrDefault(p => p.Name == "Filiquarian").Id,
                    Publisher = data.Publishers.FirstOrDefault(p => p.Name == "Filiquarian"),
                    CategoryId = 7,
                },
                new Book
                {
                    Title ="Sapiens: A Brief History of Humankind",
                    Description = "From a renowned historian comes a groundbreaking narrative of humanity’s creation and evolution—a #1 international bestseller—that explores the ways in which biology and history have defined us and enhanced our understanding of what it means to be “human.” One hundred thousand years ago, at least six different species of humans inhabited Earth. Yet today there is only one—homo sapiens. What happened to the others? And what may happen to us?",
                    Price = 24.18m,
                    ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/41+lolL22gL._SX314_BO1,204,203,200_.jpg",
                    Release = 2015,
                    AuthorId = data.Authors.FirstOrDefault(a => a.Name == "Yuval Harari").Id,
                    Author = data.Authors.FirstOrDefault(a => a.Name == "Yuval Harari"),
                    PublisherId = data.Publishers.FirstOrDefault(p => p.Name == "Dvir Publishing House Ltd.").Id,
                    Publisher = data.Publishers.FirstOrDefault(p => p.Name == "Dvir Publishing House Ltd."),
                    CategoryId = 10,
                },
                new Book
                {
                    Title ="Brave New World",
                    Description = "Aldous Huxley's profoundly important classic of world literature, Brave New World is a searching vision of an unequal, technologically-advanced future where humans are genetically bred, socially indoctrinated, and pharmaceutically anesthetized to passively uphold an authoritarian ruling order–all at the cost of our freedom, full humanity, and perhaps also our souls. “A genius [who] who spent his life decrying the onward march of the Machine” (The New Yorker), Huxley was a man of incomparable talents: equally an artist, a spiritual seeker, and one of history’s keenest observers of human nature and civilization. Brave New World, his masterpiece, has enthralled and terrified millions of readers, and retains its urgent relevance to this day as both a warning to be heeded as we head into tomorrow and as thought-provoking, satisfying work of literature. ",
                    Price = 13.99m,
                    ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/41-n-3hZMeL._SX325_BO1,204,203,200_.jpg",
                    Release = 2006,
                    AuthorId = data.Authors.FirstOrDefault(a => a.Name == "Aldous Huxley").Id,
                    Author = data.Authors.FirstOrDefault(a => a.Name == "Aldous Huxley"),
                    PublisherId = data.Publishers.FirstOrDefault(p => p.Name == "Harper Perennial").Id,
                    Publisher = data.Publishers.FirstOrDefault(p => p.Name == "Harper Perennial"),
                    CategoryId = 12,
                },
                new Book
                {
                    Title ="The Hitchhiker's Guide to the Galaxy",
                    Description = "It’s an ordinary Thursday morning for Arthur Dent . . . until his house gets demolished. The Earth follows shortly after to make way for a new hyperspace express route, and Arthur’s best friend has just announced that he’s an alien. After that, things get much, much worse. With just a towel, a small yellow fish, and a book, Arthur has to navigate through a very hostile universe in the company of a gang of unreliable aliens. Luckily the fish is quite good at languages. And the book is The Hitchhiker’s Guide to the Galaxy . . . which helpfully has the words DON’T PANIC inscribed in large, friendly letters on its cover.",
                    Price = 7.03m,
                    ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/51MzUz8rQcL._SX305_BO1,204,203,200_.jpg",
                    Release = 1995,
                    AuthorId = data.Authors.FirstOrDefault(a => a.Name == "Douglas Adams").Id,
                    Author = data.Authors.FirstOrDefault(a => a.Name == "Douglas Adams"),
                    PublisherId = data.Publishers.FirstOrDefault(p => p.Name == "Del Rey").Id,
                    Publisher = data.Publishers.FirstOrDefault(p => p.Name == "Del Rey"),
                    CategoryId = 1,
                },
                new Book
                {
                    Title ="Cell",
                    Description = "On October 1, God is in His heaven, the stock market stands at 10,140, most of the planes are on time, and graphic artist Clayton Riddell is visiting Boston, having just landed a deal that might finally enable him to make art instead of teaching it. But all those good feelings about the future change in a hurry thanks to a devastating phenomenon that will come to be known as The Pulse. The delivery method is a cell phone—everyone’s cell phone. Now Clay and the few survivors who join him find themselves in the pitch-black night of civilization’s darkest age, surrounded by chaos, carnage, and a relentless human horde that has been reduced to its basest nature...and then begins to evolve. There’s no escaping this nightmare. But for Clay, an arrow points the way home to his family in Maine, and as he and his fellow refugees make their journey north, they begin to see the crude signs confirming their direction. A promise of a safe haven, or quite possibly the deadliest trap of all....",
                    Price = 11.83m,
                    ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/51GfoUqzhKL._SX273_BO1,204,203,200_.jpg",
                    Release = 2016,
                    AuthorId = data.Authors.FirstOrDefault(a => a.Name == "Stephen King").Id,
                    Author = data.Authors.FirstOrDefault(a => a.Name == "Stephen King"),
                    PublisherId = data.Publishers.FirstOrDefault(p => p.Name == "Simon & Schuster").Id,
                    Publisher = data.Publishers.FirstOrDefault(p => p.Name == "Simon & Schuster"),
                    CategoryId = 2,
                }
            });
            data.SaveChanges();
        }
    }
}

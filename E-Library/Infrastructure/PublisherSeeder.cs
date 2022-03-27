using E_Library.Data;
using E_Library.Data.Models;

namespace E_Library.Infrastructure
{
    public static class PublisherSeeder
    {
        public static void SeedPublishers(LibraryDbContext data)
        {
            if (data.Publishers.Any())
            {
                return;
            }
            data.Publishers.AddRange(new[]
            {
                new Publisher
                {
                    Name = "Independently published",
                    Books = data.Books.Where(b => b.Publisher.Name == "Independently published").ToList(),
                },
                new Publisher
                {
                    Name = "William Morrow Paperbacks",
                    Books = data.Books.Where(b => b.Publisher.Name == "William Morrow Paperbacks").ToList(),
                },
                new Publisher
                {
                    Name = "CreateSpace Independent Publishing Platform",
                    Books = data.Books.Where(b => b.Publisher.Name == "CreateSpace Independent Publishing Platform").ToList(),
                },
                new Publisher
                {
                    Name = "Filiquarian",
                    Books = data.Books.Where(b => b.Publisher.Name == "Filiquarian").ToList(),
                },
                new Publisher
                {
                    Name = "Dvir Publishing House Ltd.",
                    Books = data.Books.Where(b => b.Publisher.Name == "Dvir Publishing House Ltd.").ToList(),
                },
                new Publisher
                {
                    Name = "Harper Perennial",
                    Books = data.Books.Where(b => b.Publisher.Name == "Harper Perennial").ToList(),
                },
                new Publisher
                {
                    Name = "Del Rey",
                    Books = data.Books.Where(b => b.Publisher.Name == "Del Rey").ToList(),
                },
                new Publisher
                {
                    Name = "Simon & Schuster",
                    Books = data.Books.Where(b => b.Publisher.Name == "Simon & Schuster").ToList(),
                }
            });
            data.SaveChanges();
        }
    }
}

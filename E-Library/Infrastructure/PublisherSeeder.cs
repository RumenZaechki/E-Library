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
                    Name = "Independently published"
                },
                new Publisher
                {
                    Name = "William Morrow Paperbacks"
                },
                new Publisher
                {
                    Name = "CreateSpace Independent Publishing Platform"
                },
                new Publisher
                {
                    Name = "Filiquarian"
                },
                new Publisher
                {
                    Name = "Dvir Publishing House Ltd."
                },
                new Publisher
                {
                    Name = "Harper Perennial"
                },
                new Publisher
                {
                    Name = "Del Rey"
                },
                new Publisher
                {
                    Name = "Simon & Schuster"
                }
            });
            data.SaveChanges();
        }
    }
}

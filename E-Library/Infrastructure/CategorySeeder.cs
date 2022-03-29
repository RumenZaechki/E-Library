using E_Library.Data;
using E_Library.Data.Models;

namespace E_Library.Infrastructure
{
    public static class CategorySeeder
    {
        public static void SeedCategories(LibraryDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }
            data.Categories.AddRange(new[]
            {
                new Category
                {
                    Name = "Fantasy and science fiction"
                },
                new Category
                {
                    Name = "Thrillers and horror"
                },
                new Category
                {
                    Name = "Biography, autobiography, and memoir"
                },
                new Category
                {
                    Name = "Action and Adventure"
                },
                new Category
                {
                    Name = "Classics"
                },
                new Category
                {
                    Name = "Detective and Mystery"
                },
                new Category
                {
                    Name = "History"
                },
                new Category
                {
                    Name = "Philosophy"
                },
                new Category
                {
                    Name = "Arts"
                },
                new Category
                {
                    Name = "Science"
                },
                new Category
                {
                    Name = "Children's"
                },
                new Category
                {
                    Name = "Fiction"
                }
            });
            data.SaveChanges();
        }
    }
}

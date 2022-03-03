using E_Library.Data;
using E_Library.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Library.Infrastructure
{
    public static class AppBuilderExtension
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var data = scopedServices.ServiceProvider.GetService<LibraryDbContext>();
            data.Database.Migrate();
            SeedCategories(data);
            return app;
        }
        private static void SeedCategories(LibraryDbContext data)
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
                    Name = "Historical Fiction"
                },
                new Category
                {
                    Name = "Philosophy"
                },
            });
            data.SaveChanges();
        }
    }
}

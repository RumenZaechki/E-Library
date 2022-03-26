using E_Library.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Library.Infrastructure
{
    public static class AppBuilderExtension
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var data = scopedServices.ServiceProvider.GetRequiredService<LibraryDbContext>();
            var serviceProvider = scopedServices.ServiceProvider;
            data.Database.Migrate();
            CategorySeeder.SeedCategories(data);
            PublisherSeeder.SeedPublishers(data);
            AuthorSeeder.SeedAuthors(data);
            BookSeeder.SeedBooks(data);
            UserSeeder.SeedAdministratior(serviceProvider);
            UserSeeder.SeedUser(serviceProvider);
            return app;
        }
    }
}
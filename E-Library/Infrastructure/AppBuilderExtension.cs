using E_Library.Data;
using E_Library.Data.Models;
using Microsoft.AspNetCore.Identity;
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
            SeedCategories(data);
            SeedAdministratior(serviceProvider);
            return app;
        }
        private static void SeedAdministratior(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync("Administrator"))
                    {
                        return;
                    }
                    await roleManager.CreateAsync(new IdentityRole
                    {
                        Name = "Administrator"
                    });
                    const string adminPassword = "admin123";
                    var user = new IdentityUser
                    {
                        Email = "admin@mail.com",
                        UserName = "admin@mail.com"
                    };
                    await userManager.CreateAsync(user, adminPassword);
                    await userManager.AddToRoleAsync(user, "Administrator");
                })
                .GetAwaiter()
                .GetResult();
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

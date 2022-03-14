using E_Library.Areas.Admin;
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
            SeedBooks(data);
            SeedAdministratior(serviceProvider);
            return app;
        }
        private static void SeedAdministratior(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(AdminConstants.AdminRoleName))
                    {
                        return;
                    }
                    await roleManager.CreateAsync(new IdentityRole
                    {
                        Name = AdminConstants.AdminRoleName
                    });
                    const string adminPassword = "admin123";
                    var user = new User
                    {
                        Email = "admin@mail.com",
                        UserName = "admin@mail.com"
                    };
                    var cart = new Cart
                    {
                        UserId = user.Id,
                        User = user,
                    };
                    user.CartId = cart.Id;
                    user.Cart = cart;
                    await userManager.CreateAsync(user, adminPassword);
                    await userManager.AddToRoleAsync(user, AdminConstants.AdminRoleName);
                })
                .GetAwaiter()
                .GetResult();
        }
        private static void SeedBooks(LibraryDbContext data)
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
                        Name = "Robert Pantano"
                    },
                    CategoryId = 3,
                },
                new Book
                {
                    Title ="Lord Of The Rings",
                    Description = "The Lord of the Rings is an epic high-fantasy novel by English author and scholar J. R. R. Tolkien. Set in Middle-earth, intended to be Earth at some distant time in the past, the story began as a sequel to Tolkien's 1937 children's book The Hobbit, but eventually developed into a much larger work.",
                    Price = 40m,
                    ImageUrl = "https://bookcorner.nyc3.digitaloceanspaces.com/uploads/original/5c72aadbecf131551018715.jpg",
                    Release = 1954,
                    AuthorId = Guid.NewGuid().ToString(),
                    Author = new Author
                    {
                        Name = "John Tolkien"
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
                        Name = "Marcus Aurelius"
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
                        Name = "Sun Tzu"
                    },
                    CategoryId = 7,
                },
            });
            data.SaveChanges();
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

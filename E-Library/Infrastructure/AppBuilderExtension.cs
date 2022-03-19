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
                        Name = "Robert Pantano",
                        Description = "Robert Pantano is the creator of the YouTube channel and production house known as Pursuit of Wonder, which covers similar topics of philosophy, science, and literature through short stories, guided experiences, video essays, and more.",
                        ImageUrl = "https://yt3.ggpht.com/ytc/AKedOLSOgx4aSr0YiJreg-4ReQwO4hKw_wbVSKcrIf5JCQ=s900-c-k-c0x00ffffff-no-rj"
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
                        Description = "Marcus Aurelius Antoninus was Roman emperor from 161 to 180 and a Stoic philosopher. He was the last of the rulers known as the Five Good Emperors (a term coined some 13 centuries later by Niccolò Machiavelli), and the last emperor of the Pax Romana, an age of relative peace and stability for the Roman Empire lasting from 27 BCE to 180 CE. He served as Roman consul in 140, 145, and 161.",
                        ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT9OsS0Bl4dOwm0DFcR5U0H8neR_fyJ9J_ePQ&usqp=CAU"
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
                },
            });
            data.SaveChanges();
        }
    }
}

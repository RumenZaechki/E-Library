using E_Library.Areas.Admin;
using E_Library.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace E_Library.Infrastructure
{
    public static class UserSeeder
    {
        public static void SeedUser(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            Task
                .Run(async () =>
                {
                    const string adminPassword = "123456";
                    var user = new User
                    {
                        Email = "user@mail.com",
                        UserName = "user@mail.com"
                    };
                    var cart = new Cart
                    {
                        UserId = user.Id,
                        User = user,
                    };
                    user.Cart = cart;
                    await userManager.CreateAsync(user, adminPassword);
                })
                .GetAwaiter()
                .GetResult();
        }
        public static void SeedAdministratior(IServiceProvider services)
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
    }
}

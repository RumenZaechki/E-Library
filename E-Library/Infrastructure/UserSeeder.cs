using E_Library.Areas.Admin;
using E_Library.Areas.Identity;
using E_Library.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace E_Library.Infrastructure
{
    public static class UserSeeder
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { AdminConstants.AdminRoleName, UserConstants.UserRoleName };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
        public static async Task SeedUser(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            const string userPassword = "123456";
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
            await userManager.CreateAsync(user, userPassword);
            await userManager.AddToRoleAsync(user, UserConstants.UserRoleName);
        }
        public static async Task SeedAdministratior(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
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
        }
    }
}

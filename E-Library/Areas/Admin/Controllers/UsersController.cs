using E_Library.Areas.Admin.Models;
using E_Library.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static E_Library.WebConstants;

namespace E_Library.Areas.Admin.Controllers
{
    public class UsersController : AdminController
    {
        private readonly IUsersService usersService;
        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }
        [Authorize]
        public async Task<IActionResult> AllUsers()
        {
            var adminId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var users = await this.usersService
                .GetAllUsersAsync(adminId);
            var res = users
                .Select(u => new UserViewModel
                {
                    Id = u.Id,
                    Username = u.Username,
                });
            return View(res);
        }

        [Authorize]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            await this.usersService.DeleteUserAsync(userId);
            this.TempData[GlobalMessageKey] = "Successfully deleted user.";
            return RedirectToAction("AllUsers", "Users", new { area = "Admin" });
        }
    }
}

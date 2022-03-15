using Microsoft.AspNetCore.Mvc;

namespace E_Library.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        public IActionResult Index() => View();
    }
}

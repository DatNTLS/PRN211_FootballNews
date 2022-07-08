using FootballNews.Logics;
using Microsoft.AspNetCore.Mvc;

namespace FootballNews.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult ManageUser()
        {
            UserManager userManager = new UserManager();
            RoleManager roleManager = new RoleManager();
            ViewBag.AllUser = userManager.GetAllUsers();
            int RoleId = 1;
            int ok = userManager.GetNumberUserByRole(RoleId);
            return View("Views/Admin/ManageUser.cshtml");
        }

        public IActionResult AddUser()
        {
            return View();
        }

        public IActionResult DeleteUser()
        {
            return View();
        }

        public IActionResult SetRoleUser()
        {
            return View();
        }

        public IActionResult ManageNews()
        {
            return View("Views/Admin/ManageNews.cshtml");
        }

        public IActionResult AddNews()
        {
            return View();
        }

        public IActionResult DeleteNews()
        {
            return View();
        }

    }
}

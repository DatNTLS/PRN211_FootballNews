using Microsoft.AspNetCore.Mvc;

namespace FootballNews.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

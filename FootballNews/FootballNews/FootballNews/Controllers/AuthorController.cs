using Microsoft.AspNetCore.Mvc;

namespace FootballNews.Controllers
{
    public class AuthorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

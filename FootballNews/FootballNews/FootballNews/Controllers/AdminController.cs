using Microsoft.AspNetCore.Mvc;

namespace FootballNews.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

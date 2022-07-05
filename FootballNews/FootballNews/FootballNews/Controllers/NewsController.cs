using FootballNews.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FootballNews.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult NewsList(int CategoryId)
        {
            using (var context = new FootballNewsContext())
            {
                ViewBag.Top5LastestNews = context.News.Where(x => x.CategoryId != 8).Take(5).OrderByDescending(x => x.DatePublished).ToList();
                ViewBag.News = context.
            }
            return View("Views/Home/NewsList.cshtml");
        }
    }
}

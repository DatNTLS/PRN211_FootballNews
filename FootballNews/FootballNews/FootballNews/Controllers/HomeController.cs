using FootballNews.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FootballNews.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            using (var context = new FootballNewsContext())
            {
                ViewBag.Top4CategoryShow = context.Categories.
                    Where(x => x.CategoryId == 1 || x.CategoryId == 2 || x.CategoryId == 3 || x.CategoryId == 8)
                    .OrderBy(x => x.CategoryId).ToList();
                ViewBag.CategoryHide = context.Categories.
                    Where(x => x.CategoryId != 1 && x.CategoryId != 2 && x.CategoryId != 3 && x.CategoryId != 8)
                    .OrderBy(x => x.CategoryId).ToList();
                ViewBag.Top5LastestNews = context.News.Where(x => x.CategoryId != 8).Take(5).OrderByDescending(x => x.DatePublished).ToList();
                ViewBag.Top5TransferNews = context.News.Where(x => x.CategoryId == 8).Take(5).OrderByDescending(x => x.DatePublished).ToList();
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

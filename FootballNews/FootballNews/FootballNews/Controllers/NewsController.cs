using FootballNews.Logics;
using FootballNews.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FootballNews.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult NewsList(int CategoryId, int Page)
        {
            CategoryManager categoryManager = new CategoryManager();
            ViewBag.Top4Categories = categoryManager.GetTop4Categories();
            ViewBag.AllOtherCategories = categoryManager.GetAllOtherCategories();

            NewsManager newsManager = new NewsManager();
            ViewBag.Top5LatestTransferNews = newsManager.GetTop5LatestTransferNews();

            if (Page <= 0)
            {
                Page = 1;
            }

            int PageSize = 2;
            ViewBag.AllNewsByCategory = newsManager.GetAllNewsByCategoryId(CategoryId, (Page - 1) * PageSize + 1, PageSize);

            int TotalNews = newsManager.GetNumberOfOrders(CategoryId);
            int TotalPage = TotalNews / PageSize;

            if (TotalNews % PageSize != 0)
            {
                TotalPage++;
            }

            ViewData["TotalPage"] = TotalPage;
            ViewData["CurrentPage"] = Page;
            ViewData["CurrentCategory"] = CategoryId;

            return View("Views/Home/NewsList.cshtml");
        }
    }
}

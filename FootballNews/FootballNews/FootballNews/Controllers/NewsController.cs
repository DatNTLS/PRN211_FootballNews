using FootballNews.Logics;
using FootballNews.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
            ViewBag.Top5LatestNews = newsManager.GetTop5LatestNews();

            if (Page <= 0)
            {
                Page = 1;
            }

            int PageSize = 5;
            ViewBag.AllNewsByCategory = newsManager.GetAllNewsByCategoryId(CategoryId, (Page - 1) * PageSize + 1, PageSize);

            int TotalNews = newsManager.GetNumberOfNews(CategoryId);
            int TotalPage = TotalNews / PageSize;

            if (TotalNews % PageSize != 0)
            {
                TotalPage++;
            }

            ViewData["TotalPage"] = TotalPage;
            ViewData["CurrentPage"] = Page;
            ViewData["CurrentCategory"] = CategoryId;

            var GetCategory = categoryManager.GetCategoryById(CategoryId);
            if (GetCategory.CategoryId == 8)
            {
                ViewData["CurrentCategory"] = "Tin " + GetCategory.CategoryName;
            }
            else
            {
                ViewData["CurrentCategory"] = "Tin Bóng Đá " + GetCategory.CategoryName;
            }

            return View("Views/News/NewsList.cshtml");
        }

        public IActionResult NewsDetails(int NewsId)
        {
            CategoryManager categoryManager = new CategoryManager();
            ViewBag.Top4Categories = categoryManager.GetTop4Categories();
            ViewBag.AllOtherCategories = categoryManager.GetAllOtherCategories();

            NewsManager newsManager = new NewsManager();
            ViewBag.Top5LatestNews = newsManager.GetTop5LatestNews();

            News n = newsManager.GetNewsById(NewsId);
            ViewData["Title"] = n.Title;
            ViewData["ShortDescription"] = n.ShortDescription;
            ViewData["Thumbnail"] = n.Thumbnail;
            ViewData["DatePublished"] = n.DatePublished;

            UserManager userManager = new UserManager();
            User u = userManager.GetUserByAuthorId(n.AuthorId.Value);
            ViewData["AuthorName"] = u.UserName;
            if (u.Avatar.Equals(""))
            {
                ViewData["Avatar"] = "2120b058cb9946e36306778243eadae5.jpg";
            }
            else
            {
                ViewData["Avatar"] = u.Avatar;
            }

            ImageManager imageManager = new ImageManager();
            ViewBag.AllImages = imageManager.GetAllImagesByNewsId(n.NewsId);

            ContentManager contentManager = new ContentManager();
            ViewBag.AllContents = contentManager.GetAllContents();

            return View("Views/News/NewsDetails.cshtml");
        }


        public IActionResult SearchNews(string NewsValue)
        {

            return View("Views/News/NewsList.cshtml");
        }
    }
}

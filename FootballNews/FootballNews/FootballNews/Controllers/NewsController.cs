using FootballNews.Logics;
using FootballNews.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            ViewBag.AllNewsByCategory = newsManager.GetAllNewsByCategoryId(CategoryId, (Page - 1) * PageSize+1, PageSize);

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
                ViewData["CategoryName"] = "Tin " + GetCategory.CategoryName;
            }
            else
            {
                ViewData["CategoryName"] = "Tin Bóng Đá " + GetCategory.CategoryName;
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
            ViewData["NewsId"] = n.NewsId;
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

            CommentManager commentManager = new CommentManager();
            ViewBag.AllComments = commentManager.GetAllComments(n.NewsId);
            ViewData["NumberOfComment"] = commentManager.GetAllComments(n.NewsId).Count;

            ViewBag.AllUsers = userManager.GetAllUsers();

            return View("Views/News/NewsDetails.cshtml");
        }

        [HttpPost]
        public IActionResult SearchNews(string NewsValue)
        {
            CategoryManager categoryManager = new CategoryManager();
            ViewBag.Top4Categories = categoryManager.GetTop4Categories();
            ViewBag.AllOtherCategories = categoryManager.GetAllOtherCategories();

            NewsManager newsManager = new NewsManager();
            ViewBag.Top5LatestNews = newsManager.GetTop5LatestNews();

            newsManager.SearchNews(NewsValue);
            return View("Views/News/NewsList.cshtml");
        }

        //Comment Action
        [HttpPost]
        public IActionResult AddComment(int NewsId, int UserId, string Comment)
        {
            CommentManager commentManager = new CommentManager();
            if (ModelState.IsValid)
            {
                if (HttpContext.Session.GetString("CurrentUser") == null)
                {
                    return RedirectToAction("Login", "User", new { Area = "" });
                }
                else
                {
                    commentManager.AddComment(NewsId, UserId, Comment);
                    return RedirectToAction("NewsDetails", "News", new {NewsId = NewsId});
                }
            }
            return View();
        }

    }
}

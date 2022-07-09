using FootballNews.Logics;
using FootballNews.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace FootballNews.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Error()
        {
            return View("Views/Admin/Error.cshtml");
        }

        public IActionResult ManageUser()
        {
            User CurrentUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("CurrentUser"));
            if (CurrentUser.RoleId != 1)
            {
                return Error();
            }
            else
            {
                UserManager userManager = new UserManager();
                RoleManager roleManager = new RoleManager();

                List<User> users = userManager.GetAllUsers();
                ViewBag.AllUsers = users;

                List<Role> roles = roleManager.GetAllRoles();
                ViewBag.AllRoles = roles;

                ViewData["NumberAdmin"] = userManager.GetNumberUserByRole(1);
                ViewData["NumberJournalist"] = userManager.GetNumberUserByRole(2);
                ViewData["NumberReader"] = userManager.GetNumberUserByRole(3);
                ViewData["TotalUser"] = users.Count;

                return View("Views/Admin/ManageUser.cshtml");
            }

        }

        public IActionResult AddUser(string Avatar, string Username, string Email, string Password, int Role)
        {
            UserManager userManager = new UserManager();

            if (userManager.GetUserByName(Username) != null)
            {
                ViewBag.Error1 = "Tên người dùng đã được sử dụng !";
            }
            if (userManager.GetUserByEmail(Email) != null)
            {
                ViewBag.Error2 = "Địa chỉ email đã được sử dụng !";
            }
            if (userManager.GetUserByName(Username) != null || userManager.GetUserByEmail(Email) != null)
            {
                return ManageUser();
            }
            else
            {
                userManager.AddUser(Avatar, Username, Email, Password, Role);
                return RedirectToAction("ManageUser", "Admin");
            }

        }

        public IActionResult DeleteUser(int UserId)
        {
            UserManager userManager = new UserManager();

            User CurrentUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("CurrentUser"));
            if (CurrentUser.RoleId != 1)
            {
                return Error();
            }
            else
            {
                userManager.DeleteUser(UserId);
                return RedirectToAction("ManageUser", "Admin");
            }

        }

        public IActionResult SetRoleUser(int SetRole, int UserId)
        {
            UserManager userManager = new UserManager();

            User CurrentUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("CurrentUser"));
            if (CurrentUser.RoleId != 1)
            {
                return Error();
            }
            else
            {
                userManager.SetRole(SetRole, UserId);
                return RedirectToAction("ManageUser", "Admin");
            }

        }

        public IActionResult ManageNews(int CategoryId,int Page)
        {
            User CurrentUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("CurrentUser"));
            if (CurrentUser.RoleId != 1)
            {
                return Error();
            }
            else
            {
                NewsManager newsManager = new NewsManager();
                UserManager userManager = new UserManager();
                CategoryManager categoryManager = new CategoryManager();
                int PageSize = 10;
                ViewBag.AllNews = newsManager.GetAllNewsByCategoryId(CategoryId, (Page - 1) * PageSize + 1, PageSize);
                ViewBag.AllUsers = userManager.GetAllUsers();
                ViewBag.AllCategories = categoryManager.GetAllCategories();
                int TotalNews = newsManager.GetNumberOfNews(CategoryId);
                int TotalPage = TotalNews / PageSize;

                if (TotalNews % PageSize != 0)
                {
                    TotalPage++;
                }

                ViewData["TotalPage"] = TotalPage;
                ViewData["CurrentPage"] = Page;
                ViewData["CurrentCategory"] = 0;

                return View("Views/Admin/ManageNews.cshtml");

            }
        }

        public IActionResult AddNews()
        {

            return View();
        }

        public IActionResult DeleteNews(int NewsId)
        {
            NewsManager newsManager = new NewsManager();
            ImageManager imageManager = new ImageManager();
            ContentManager contentManager = new ContentManager();
            CommentManager commentManager = new CommentManager();

            var context = new FootballNewsContext();
            contentManager.DeleteContentById(NewsId);
            imageManager.DeleteImageById(NewsId);
            newsManager.DeleteNewsById(NewsId);
            commentManager.DeleteCommentByNewsId(NewsId);
            
            return RedirectToAction("ManageNews", "Admin");
        }

        [HttpGet]
        public IActionResult UpdateNews(int NewsId)
        {
            return View();
        }

    }
}

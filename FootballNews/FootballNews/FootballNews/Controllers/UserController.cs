using FootballNews.Logics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballNews.Controllers
{


    public class UserController : Controller
    {

        [HttpGet]
        public IActionResult Login()
        {
            return View("Views/User/Login.cshtml");
        }

        [HttpPost]
        public IActionResult Login(string Username, string Password)
        {
            UserManager userManager = new UserManager();
            if (ModelState.IsValid)
            {
                var CheckUserName = userManager.GetUserByName(Username);
                if (CheckUserName == null)
                {
                    ViewBag.Error = "Tên người dùng không tồn tại !";
                    return Login();
                }
                else
                {
                    var CheckPassword = userManager.GetUserByPassword(Password);
                    if (CheckPassword == null)
                    {
                        ViewBag.Error = "Mật khẩu không chính xác !";
                        return Login();
                    }
                    else
                    {
                        if (CheckUserName.Status == false)
                        {

                        }
                        else
                        {
                            HttpContext.Session.SetString("CurrentUser", JsonConvert.SerializeObject(CheckUserName));
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Remove("CurrentUser");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("Views/User/Register.cshtml");
        }

        [HttpPost]
        public IActionResult Register(string Username, string Email, string Password, string ConfirmPassword)
        {
            UserManager userManager = new UserManager();
            if (ModelState.IsValid)
            {
                var CheckUserName = userManager.GetUserByName(Username);
                if (CheckUserName != null)
                {
                    ViewBag.Error1 = "Tên người dùng không tồn tại !";
                }

                var CheckEmail = userManager.GetUserByEmail(Email);
                if (CheckEmail != null)
                {
                    ViewBag.Error2 = "Tên người dùng không tồn tại !";
                }

                if (!Password.Equals(ConfirmPassword))
                {
                    ViewBag.Error3 = "Tên người dùng không tồn tại !";
                }

                if (CheckUserName != null || CheckEmail != null || !Password.Equals(ConfirmPassword))
                {
                    return Register();
                } else
                {
                    EmailSender emailSender = new EmailSender();
                    String Otp = emailSender.GenerateRandomNumber();
                    userManager.InsertUser(Username, Email, Password, Otp);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Verify()
        {
            return View("Views/Home/Verify.cshtml");
        }

        [HttpPost]
        public IActionResult Verify(string Otp)
        {
            UserManager userManager = new UserManager();
            if (ModelState.IsValid)
            {
                
            }
            return View();
        }
    }
}

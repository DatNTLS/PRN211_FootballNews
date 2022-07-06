using FootballNews.Logics;
using FootballNews.Models;
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

        //Login Screen
        [HttpGet]
        public IActionResult Login()
        {
            return View("Views/User/Login.cshtml");
        }

        //Login Action
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
                            HttpContext.Session.SetString("CurrentEmail", CheckUserName.Email);
                            return RedirectToAction("Verify", "User");
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

        //Logout Action
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("CurrentUser");
            return RedirectToAction("Index", "Home");
        }

        //Register Screen
        [HttpGet]
        public IActionResult Register()
        {
            return View("Views/User/Register.cshtml");
        }

        //Register Action
        [HttpPost]
        public IActionResult Register(string Username, string Email, string Password, string ConfirmPassword)
        {
            UserManager userManager = new UserManager();
            if (ModelState.IsValid)
            {
                var CheckUserName = userManager.GetUserByName(Username);
                if (CheckUserName != null)
                {
                    ViewBag.Error1 = "Tên người dùng đã được sử dụng !";
                }

                var CheckEmail = userManager.GetUserByEmail(Email);
                if (CheckEmail != null)
                {
                    ViewBag.Error2 = "Địa chỉ email đã được sử dụng !";
                }

                if (!Password.Equals(ConfirmPassword))
                {
                    ViewBag.Error3 = "Mật khẩu và xác nhận mật khẩu không khớp !";
                }

                if (CheckUserName != null || CheckEmail != null || !Password.Equals(ConfirmPassword))
                {
                    return Register();
                }
                else
                {
                    EmailSender emailSender = new EmailSender();
                    String Otp = emailSender.GenerateRandomNumber();

                    userManager.InsertUser(Username, Email, Password, Otp);

                    String HtmlContent = "<h2>Xin chào " + Email + " ,</h2>" +
                        "<p>Chúng tôi đã gửi 1 đoạn mã đến Email của bạn.<br><br>" +
                        "Hãy sử dụng mã này Để xác nhận tài khoản.<br><br>" +
                        "Mã xác nhận : <span style='font - weight: bold; '>" + Otp + "</span><br><br>" +
                        "Vui lòng không chia sẻ mã này với bất kì ai.</p>";

                    string FromEmail = "quizpracticeg6@gmail.com";
                    string GetPassword = "mrxexghqvwyekhqk";

                    HttpContext.Session.SetString("CurrentEmail", Email);

                    emailSender.SendEmail(FromEmail, GetPassword, Email, "Xác Nhận Tài Khoản", HtmlContent);

                    return RedirectToAction("Verify", "User");
                }
            }
            return View();
        }

        //Verify Screen
        [HttpGet]
        public IActionResult Verify()
        {
            return View("Views/User/Verify.cshtml");
        }

        //Verify Action
        [HttpPost]
        public IActionResult Verify(string Otp)
        {
            UserManager userManager = new UserManager();
            if (ModelState.IsValid)
            {
                string CurrentEmail = HttpContext.Session.GetString("CurrentEmail");
                string EmailFG = HttpContext.Session.GetString("EmailFG");
                if (CurrentEmail != null)
                {
                    User CheckOTP = userManager.CheckOTP(CurrentEmail, Otp);
                    if (CheckOTP == null)
                    {
                        ViewBag.Error = "Mã xác nhận không chính xác !";
                        return Verify();
                    }
                    else
                    {
                        userManager.UpdateStatus(CurrentEmail, true);
                        userManager.UpdateOtp(CurrentEmail, "");
                        HttpContext.Session.Remove("CurrentEmail");
                        return RedirectToAction("Login", "User");
                    }
                }

                if (EmailFG != null)
                {
                    User CheckOTP = userManager.CheckOTP(EmailFG, Otp);
                    if (CheckOTP == null)
                    {
                        ViewBag.Error = "Mã xác nhận không chính xác !";
                        return Verify();
                    }
                    else
                    {
                        return RedirectToAction("ChangePassword", "User");
                    }
                }

            }
            return View();
        }

        //Resend OTP Action
        public IActionResult Resend()
        {
            UserManager userManager = new UserManager();
            if (ModelState.IsValid)
            {
                EmailSender emailSender = new EmailSender();
                string CurrentEmail = HttpContext.Session.GetString("CurrentEmail");
                string NewOtp = emailSender.GenerateRandomNumber();
                String HtmlContent = "<h2>Xin chào " + CurrentEmail + " ,</h2>" +
                        "<p>Chúng tôi đã gửi 1 đoạn mã đến Email của bạn.<br><br>" +
                        "Hãy sử dụng mã này Để xác nhận tài khoản.<br><br>" +
                        "Mã xác nhận : <span style='font - weight: bold; '>" + NewOtp + "</span><br><br>" +
                        "Vui lòng không chia sẻ mã này với bất kì ai.</p>";

                string FromEmail = "quizpracticeg6@gmail.com";
                string GetPassword = "mrxexghqvwyekhqk";

                emailSender.SendEmail(FromEmail, GetPassword, CurrentEmail, "Xác Nhận Tài Khoản", HtmlContent);
                userManager.UpdateOtp(CurrentEmail, NewOtp);

                return RedirectToAction("Verify", "User");
            }
            return View();
        }

        //Forgot Password Screen
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View("Views/User/ForgotPassword.cshtml");
        }

        //Forgot Password Action
        [HttpPost]
        public IActionResult ForgotPassword(string Email)
        {
            UserManager userManager = new UserManager();
            if (ModelState.IsValid)
            {
                User CheckEmail = userManager.GetUserByEmail(Email);
                if (CheckEmail == null)
                {
                    ViewBag.Error = "Email không tồn tại trong hệ thống !";
                    return ForgotPassword();
                }
                else
                {
                    EmailSender emailSender = new EmailSender();
                    string NewOtp = emailSender.GenerateRandomNumber();
                    String HtmlContent = "<h2>Xin chào " + Email + " ,</h2>" +
                            "<p>Chúng tôi đã gửi 1 đoạn mã đến Email của bạn.<br><br>" +
                            "Hãy sử dụng mã này Để xác nhận và đổi mật khẩu của bạn.<br><br>" +
                            "Mã xác nhận : <span style='font - weight: bold; '>" + NewOtp + "</span><br><br>" +
                            "Vui lòng không chia sẻ mã này với bất kì ai.</p>";

                    string FromEmail = "quizpracticeg6@gmail.com";
                    string GetPassword = "mrxexghqvwyekhqk";

                    emailSender.SendEmail(FromEmail, GetPassword, Email, "Đổi Mật Khẩu", HtmlContent);
                    userManager.UpdateOtp(Email, NewOtp);
                    HttpContext.Session.SetString("EmailFG", Email);

                    return RedirectToAction("Verify", "User");
                }
            }
            return View();
        }

        //Change Password Screen
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View("Views/User/ChangePassword.cshtml");
        }

        //Change Password Action
        [HttpPost]
        public IActionResult ChangePassword(string Email, string Password, string ConfirmPassword)
        {
            UserManager userManager = new UserManager();
            if (ModelState.IsValid)
            {
                if (!Password.Equals(ConfirmPassword))
                {
                    ViewBag.Error = "Mật khẩu không khớp với nhau !";
                    return ChangePassword();
                }
                else
                {
                    userManager.UpdatePassword(Email, Password);
                    userManager.UpdateOtp(Email, "");
                    HttpContext.Session.Remove("EmailFG");
                    return RedirectToAction("Login", "User");
                }
            }
            return View();
        }
    }
}

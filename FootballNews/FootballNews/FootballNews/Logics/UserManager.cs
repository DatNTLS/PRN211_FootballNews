using FootballNews.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballNews.Logics
{
    public class UserManager
    {
        public User GetUserByName(string Username)
        {
            using (var context = new FootballNewsContext())
            {
                return context.Users.Where(x => x.UserName == Username).FirstOrDefault();
            }
        }

        public int GetNumberUserByRole(int RoleId)
        {
            using (var context = new FootballNewsContext())
            {
                return context.Users.Where(x => x.RoleId == RoleId).Count();
            }
        }

        public User GetUserByEmail(string Email)
        {
            using (var context = new FootballNewsContext())
            {
                return context.Users.Where(x => x.Email == Email).FirstOrDefault();
            }
        }

        public User CheckLogin(string Username, string Password)
        {
            using (var context = new FootballNewsContext())
            {
                return context.Users.Where(x => x.UserName == Username && x.Password == Password).FirstOrDefault();
            }
        }

        public void InsertUser(string Username, string Email, string Password, string Otp)
        {
            using (var context = new FootballNewsContext())
            {
                User user = new User { UserName = Username, Email = Email, Password = Password, Avatar = ""
                    ,  RoleId = 3, Otp = Otp, Status = false
                };
                context.Add(user);
                context.SaveChanges();
            }
        }

        public User GetUserByAuthorId(int AuthorId)
        {
            using (var context = new FootballNewsContext())
            {
                return context.Users.Where(x => x.UserId == AuthorId).FirstOrDefault();
            }
        }

        public User CheckOTP(string Email,string Otp)
        {
            using (var context = new FootballNewsContext())
            {
                return context.Users.Where(x => x.Email == Email && x.Otp == Otp).FirstOrDefault();
            }
        }

        public void UpdateStatus(string Email, bool Status)
        {

            using (var context = new FootballNewsContext())
            {
                User CurrentUser = context.Users.Where(x => x.Email == Email).FirstOrDefault();
                CurrentUser.Status = Status;
                context.SaveChanges();
            }
        }

        public void UpdateOtp(string Email, string Otp)
        {
            using (var context = new FootballNewsContext())
            {
                User CurrentUser = context.Users.Where(x => x.Email == Email).FirstOrDefault();
                CurrentUser.Otp = Otp;
                context.SaveChanges();
            }
        }

        public void UpdatePassword(string Email, string Password)
        {
            using (var context = new FootballNewsContext())
            {
                User CurrentUser = context.Users.Where(x => x.Email == Email).FirstOrDefault();
                CurrentUser.Password = Password;
                context.SaveChanges();
            }
        }

        public List<User> GetAllUsers()
        {
            using (var context = new FootballNewsContext())
            {
                return context.Users.ToList();
            }
        }

        public void UpdateUserProfile(string Avatar, string Username, string Email)
        {
            using (var context = new FootballNewsContext())
            {
                User CurrentUser = context.Users.Where(x => x.Email == Email).FirstOrDefault();
                CurrentUser.UserName = Username;
                CurrentUser.Avatar = Avatar;
                context.SaveChanges();
            }
        }
    }
}

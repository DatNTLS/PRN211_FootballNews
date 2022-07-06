using FootballNews.Models;
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


        public User GetUserByEmail(string Email)
        {
            using (var context = new FootballNewsContext())
            {
                return context.Users.Where(x => x.Email == Email).FirstOrDefault();
            }
        }

        public User GetUserByPassword(string Password)
        {
            using (var context = new FootballNewsContext())
            {
                return context.Users.Where(x => x.Password == Password).FirstOrDefault();
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
    }
}

using FootballNews.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballNews.Logics
{
    public class ContentManager
    {
        public List<Content> GetAllContents()
        {
            using (var context = new FootballNewsContext())
            {
                return context.Contents.ToList();
            }
        }

        public void DeleteContentById(int NewsId)
        {
            using (var context = new FootballNewsContext())
            {
                List<Content> ct = context.Contents.Where(x => x.Image.NewsId == NewsId).ToList();
                foreach (Content c in ct)
                {
                    context.Remove(c);
                    context.SaveChanges();
                }
            }
        }
    }
}

using FootballNews.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballNews.Logics
{
    public class ContentManager
    {
        internal List<Content> GetAllContents()
        {
            using (var context = new FootballNewsContext())
            {
                return context.Contents.ToList();
            }
        }
    }
}

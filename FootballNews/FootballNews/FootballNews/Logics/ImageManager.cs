using FootballNews.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballNews.Logics
{
    public class ImageManager
    {

        public List<Image> GetAllImagesByNewsId(int NewsId)
        {
            using (var context = new FootballNewsContext())
            {
                return context.Images.Where(x => x.NewsId == NewsId).ToList();
            }
        }
    }
}

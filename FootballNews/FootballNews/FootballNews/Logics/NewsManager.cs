using FootballNews.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballNews.Logics
{
    public class NewsManager
    {
        public List<News> GetTop5LatestNews()
        {
            using (var context = new FootballNewsContext())
            {
                return context.News.Take(5).OrderByDescending(x => x.DatePublished).ToList();
            }
        }

        public List<News> GetTop5LatestTransferNews()
        {
            using (var context = new FootballNewsContext())
            {
                return context.News.Where(x => x.CategoryId == 8).Take(5).OrderByDescending(x => x.DatePublished).ToList();
            }
        }

        public List<News> GetAllNewsByCategoryId(int CategoryId, int Offset, int Count)
        {
            using (var context = new FootballNewsContext())
            {
                return context.News.Where(x => x.CategoryId == CategoryId).Skip(Offset - 1).Take(Count).OrderByDescending(x => x.DatePublished).ToList();
            }
        }

        public int GetNumberOfNews(int CategoryId)
        {
            using (var context = new FootballNewsContext())
            {

                return context.News.Where(x => x.CategoryId == CategoryId).Count();
            }
        }

        public News GetNewsById(int NewsId)
        {
            using (var context = new FootballNewsContext())
            {
                return context.News.Where(x => x.NewsId == NewsId).FirstOrDefault();
            }
        }

        public List<News> SearchNews(string NewsValue)
        {
            using (var context = new FootballNewsContext())
            {
                return context.News.Where(x => x.Title.Equals(NewsValue)).OrderByDescending(x => x.DatePublished).ToList();
            }
        }
    }
}

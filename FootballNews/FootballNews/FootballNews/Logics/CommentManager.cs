﻿using FootballNews.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballNews.Logics
{
    public class CommentManager
    {
        public List<Comment> GetAllComments(int NewsId)
        {
            using (var context = new FootballNewsContext())
            {
                return context.Comments.Where(x => x.NewsId == NewsId)
                    .OrderByDescending(x => x.CommentId).ToList();

            }
        }

        public void AddComment(int newsId, int userId, string comment)
        {
            using (var context = new FootballNewsContext())
            {
                Comment c = new Comment
                {
                    UserId = userId,
                    NewsId = newsId,
                    Content = comment
                };
                context.Add(c);
                context.SaveChanges();
            }
        }
    }
}

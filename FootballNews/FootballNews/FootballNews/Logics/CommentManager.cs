using FootballNews.Models;
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
    }
}

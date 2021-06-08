using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Models;
using System;
using System.Linq;

namespace UnitTest.DAL
{
    public class TestNewsCommentLikeDal : TestMilliGazeteDbContext
    {
        public readonly INewsCommentLikeDal newsCommentLikeDal;
        public TestNewsCommentLikeDal()
        {
            if (newsCommentLikeDal == null)
                newsCommentLikeDal = MockNewsCommentLikeDal();
        }

        INewsCommentLikeDal MockNewsCommentLikeDal()
        {
            var comments = db.NewsComment.ToList();
            db.NewsComment.RemoveRange(comments);
            var list = db.NewsCommentLike.ToList();
            db.NewsCommentLike.RemoveRange(list);
            for (int i = 1; i <= dataCount; i++)
            {
                db.NewsComment.Add(new NewsComment()
                {
                    Id = i,
                    CreatedAt = DateTime.Now.AddDays(-i * 2),
                    Deleted = i > dataCount - 2,
                    Approved = i < 5,
                    Content = "Content_" + i,
                    NewsId = i < 3 ? 1 : i,
                    Title = "Title_" + i,
                    UserId = i,
                    TotalLikeCount = i + 2
                });
                db.NewsCommentLike.Add(new NewsCommentLike()
                {
                    Id = i,
                    CreatedAt = DateTime.Now.AddDays(-i * 2),
                    IsLike = true,
                    NewsCommentId = i,
                    UserId = i
                });
                db.SaveChanges();
            }
            return new EfNewsCommentLikeDal(db);

        }
    }
}

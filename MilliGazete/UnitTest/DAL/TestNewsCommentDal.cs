using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Models;
using System;
using System.Linq;

namespace UnitTest.DAL
{
    public class TestNewsCommentDal : TestMilliGazeteDbContext
    {
        public readonly INewsCommentDal newsCommentDal;
        public TestNewsCommentDal()
        {
            if (newsCommentDal == null)
                newsCommentDal = MockNewsCommentDal();
        }

        INewsCommentDal MockNewsCommentDal()
        {
            var list = db.NewsComments.ToList();
            db.NewsComments.RemoveRange(list);
            for (int i = 1; i <= dataCount; i++)
            {
                db.NewsComments.Add(new NewsComment()
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
                db.SaveChanges();
            }
            return new EfNewsCommentDal(db);

        }
    }
}

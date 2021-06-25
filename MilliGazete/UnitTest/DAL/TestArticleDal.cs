using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Models;
using System;
using System.Linq;

namespace UnitTest.DAL
{
    public class TestArticleDal : TestMilliGazeteDbContext
    {
        public readonly IArticleDal articleDal;
        public TestArticleDal()
        {
            if (articleDal == null)
                articleDal = MockArticleDal();
        }

        IArticleDal MockArticleDal()
        {
            var list = db.Articles.ToList();
            db.Articles.RemoveRange(list);
            for (int i = 1; i <= dataCount; i++)
            {
                db.Articles.Add(new Article()
                {
                    Approved = i < 8,
                    Id = i,
                    CreatedAt = DateTime.Now.AddDays(-i * 2),
                    Deleted = i > dataCount - 2,
                    Url = "Url " + i,
                    SeoKeywords = "keys " + i,
                    SeoDescription = "Seo Desc" + i,
                    Title = "Article Name " + i,
                    SeoTitle = "Style Code " + i,
                    AuthorId = i,
                    HtmlContent = "<p>test " + i + "</p>",
                    ReadCount = i
                });
                db.SaveChanges();
            }
            return new EfArticleDal(db);

        }
    }
}

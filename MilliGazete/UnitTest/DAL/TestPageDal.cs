using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Models;
using System;
using System.Linq;

namespace UnitTest.DAL
{
    public class TestPageDal : TestMilliGazeteDbContext
    {
        public readonly IPageDal pageDal;
        public TestPageDal()
        {
            if (pageDal == null)
                pageDal = MockPageDal();
        }

        IPageDal MockPageDal()
        {
            var list = db.Pages.ToList();
            db.Pages.RemoveRange(list);
            for (int i = 1; i <= dataCount; i++)
            {
                db.Pages.Add(new Page()
                {
                    Id = i,
                    CreatedAt = DateTime.Now.AddDays(-i * 2),
                    Deleted = i > dataCount - 2,
                    Url = "Url " + i,
                    SeoKeywords = "keys " + i,
                    SeoDescription = "Seo Desc" + i,
                    HtmlContent = "html " + i,
                    SeoTitle = "seo " + i,
                    Title = "title " + i
                });
                db.SaveChanges();
            }
            return new EfPageDal(db);

        }
    }
}

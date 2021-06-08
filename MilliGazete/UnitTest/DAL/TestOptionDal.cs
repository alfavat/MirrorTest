using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Models;
using System.Linq;

namespace UnitTest.DAL
{
    public class TestOptionDal : TestMilliGazeteDbContext
    {
        public IOptionDal optionDal;
        public TestOptionDal()
        {
            if (optionDal == null)
                optionDal = MockOptionDal();
        }

        IOptionDal MockOptionDal()
        {
            var list = db.Option.ToList();
            db.Option.RemoveRange(list);
            for (int i = 1; i <= dataCount; i++)
            {
                db.Option.Add(new Option()
                {
                    Id = i,
                    Address = "Address " + i,
                    Email = "test" + i + "@test.com",
                    SeoKeywords = "keys " + i,
                    SeoDescription = "Seo Desc" + i,
                    Fax = "123657" + i,
                    PageRefreshPeriod = i % 2 == 0 ? (int?)null : i,
                    Telephone = "054333254784" + i,
                    WebsiteSlogan = "slogan " + i,
                    WebsiteTitle = "title " + i
                });
                db.SaveChanges();
            }
            return new EfOptionDal(db);
        }
    }
}

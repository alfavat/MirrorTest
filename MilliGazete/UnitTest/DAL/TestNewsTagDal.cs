using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Models;
using System.Linq;

namespace UnitTest.DAL
{
    public class TestNewsTagDal : TestMilliGazeteDbContext
    {
        public INewsTagDal newsTagDal;
        public TestNewsTagDal()
        {
            if (newsTagDal == null)
                newsTagDal = MockNewsTagDal();
        }

        INewsTagDal MockNewsTagDal()
        {
            var list = db.NewsTag.ToList();
            db.NewsTag.RemoveRange(list);
            for (int i = 1; i <= dataCount; i++)
            {
                db.NewsTag.Add(new NewsTag()
                {
                    Id = i,
                    NewsId = i,
                    TagId = i
                });
                db.SaveChanges();
            }
            return new EfNewsTagDal(db);
        }


    }
}

using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Enums;
using Entity.Models;
using System;
using System.Linq;

namespace UnitTest.DAL
{
    public class TestNewsHitDal : TestMilliGazeteDbContext
    {
        public readonly INewsHitDal newsHitDal;
        public TestNewsHitDal()
        {
            if (newsHitDal == null)
                newsHitDal = MockNewsHitDal();
        }

        INewsHitDal MockNewsHitDal()
        {
            var list = db.NewsHits.ToList();
            db.NewsHits.RemoveRange(list);
            for (int i = 1; i <= dataCount; i++)
            {
                db.NewsHits.Add(new NewsHit()
                {
                    Id = i,
                    CreatedAt = DateTime.Now.AddDays(-i * 2),
                    NewsId = i,
                    NewsHitComeFromEntityId = i < 5 ? (int)NewsHitEntities.MainPageRedirect : (int)NewsHitEntities.SearchEngineRedirc
                });
                db.SaveChanges();
            }
            return new EfNewsHitDal(db);

        }
    }
}

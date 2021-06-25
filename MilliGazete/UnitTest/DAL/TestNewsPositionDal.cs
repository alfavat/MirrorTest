using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Models;
using System.Linq;

namespace UnitTest.DAL
{
    public class TestNewsPositionDal : TestMilliGazeteDbContext
    {
        public INewsPositionDal newsPositionDal;

        public TestNewsPositionDal()
        {
            if (newsPositionDal == null)
                newsPositionDal = MockNewsPositionDal();
        }

        INewsPositionDal MockNewsPositionDal()
        {
            var list = db.NewsPositions.ToList();
            db.NewsPositions.RemoveRange(list);
            for (int i = 1; i <= dataCount; i++)
            {
                db.NewsPositions.Add(new NewsPosition()
                {
                    Id = i,
                    PositionEntityId = i < 5 ? 1 : i,
                    NewsId = i,
                    Order = i < 5 ? i : i - 4,
                    Value = i % 2 == 0
                });
                db.SaveChanges();
            }
            return new EfNewsPositionDal(db);
        }
    }
}

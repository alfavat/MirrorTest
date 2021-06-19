using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Models;
using System;
using System.Linq;

namespace UnitTest.DAL
{
    public class TestReporterDal : TestMilliGazeteDbContext
    {
        public readonly IReporterDal reporterDal;
        public TestReporterDal()
        {
            if (reporterDal == null)
                reporterDal = MockReporterDal();
        }

        IReporterDal MockReporterDal()
        {
            var list = db.Reporters.ToList();
            db.Reporters.RemoveRange(list);
            for (int i = 1; i <= dataCount; i++)
            {
                db.Reporters.Add(new Reporter()
                {
                    Id = i,
                    FullName = "Reporter Name " + i.ToString(),
                    Deleted = i > dataCount - 2,
                });
                db.SaveChanges();
            }
            return new EfReporterDal(db);

        }
    }
}

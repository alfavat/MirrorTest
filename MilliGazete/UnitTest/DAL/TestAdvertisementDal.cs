using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Models;
using System;
using System.Linq;

namespace UnitTest.DAL
{
    public class TestAdvertisementDal : TestMilliGazeteDbContext
    {
        public readonly IAdvertisementDal advertisementDal;
        public TestAdvertisementDal()
        {
            if (advertisementDal == null)
                advertisementDal = MockAdvertisementDal();
        }

        IAdvertisementDal MockAdvertisementDal()
        {
            var list = db.Advertisement.ToList();
            db.Advertisement.RemoveRange(list);
            for (int i = 1; i <= dataCount; i++)
            {
                db.Advertisement.Add(new Advertisement()
                {
                    Active = i < 8,
                    Id = i,
                    CreatedAt = DateTime.Now.AddDays(-i * 2),
                    Deleted = i > dataCount - 2,
                    Description = "desc " + i,
                    GoogleId = "gid_" + i,
                    Height = i * 100,
                    Key = "key_" + i,
                    Width = i * 50
                });
                db.SaveChanges();
            }
            return new EfAdvertisementDal(db);

        }
    }
}

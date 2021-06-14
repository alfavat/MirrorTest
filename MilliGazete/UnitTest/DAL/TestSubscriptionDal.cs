using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Models;
using System;
using System.Linq;

namespace UnitTest.DAL
{
    public class TestSubscriptionDal : TestMilliGazeteDbContext
    {
        public readonly ISubscriptionDal subscriptionDal;
        public TestSubscriptionDal()
        {
            if (subscriptionDal == null)
                subscriptionDal = MockSubscriptionDal();
        }

        ISubscriptionDal MockSubscriptionDal()
        {
            var list = db.Subscriptions.ToList();
            db.Subscriptions.RemoveRange(list);
            for (int i = 1; i <= dataCount; i++)
            {
                db.Subscriptions.Add(new Subscription()
                {
                    Id = i,
                    CreatedAt = DateTime.Now.AddDays(-i * 2),
                    Email = i.ToString() + "shakibait@gmail.com",
                    FullName = "Saeid Shakiba request:" + i.ToString(),
                    Description = "This is Description " + i.ToString(),
                    Phone = "05524353057" + i.ToString(),
                    Address = "Address " + i.ToString(),
                    CityId = 34,
                    DistrictId = 423
                });
                db.SaveChanges();
            }
            return new EfSubscriptionDal(db);

        }
    }
}

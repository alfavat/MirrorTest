using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Models;
using System;
using System.Linq;

namespace UnitTest.DAL
{
    public class TestContactDal : TestMilliGazeteDbContext
    {
        public readonly IContactDal contactDal;
        public TestContactDal()
        {
            if (contactDal == null)
                contactDal = MockContactDal();
        }

        IContactDal MockContactDal()
        {
            var list = db.Contacts.ToList();
            db.Contacts.RemoveRange(list);
            for (int i = 1; i <= dataCount; i++)
            {
                db.Contacts.Add(new Contact()
                {
                    Id = i,
                    CreatedAt = DateTime.Now.AddDays(-i * 2),
                    Email = i.ToString() + "shakibait@gmail.com",
                    FullName = "Saeid Shakiba request:" + i.ToString(),
                    Message = "This is message " + i.ToString(),
                    Phone = "05524353057" + i.ToString()
                });
                db.SaveChanges();
            }
            return new EfContactDal(db);

        }
    }
}

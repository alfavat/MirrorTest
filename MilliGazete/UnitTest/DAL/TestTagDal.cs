using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Models;
using System;
using System.Linq;

namespace UnitTest.DAL
{
    public class TestTagDal : TestMilliGazeteDbContext
    {
        public ITagDal tagDal;
        public TestTagDal()
        {
            if (tagDal == null)
                tagDal = MockTagDal();
        }

        ITagDal MockTagDal()
        {
            var list = db.Tag.ToList();
            db.Tag.RemoveRange(list);
            for (int i = 1; i <= dataCount; i++)
            {
                db.Tag.Add(new Tag()
                {
                    Active = i < 8,
                    Id = i,
                    CreatedAt = DateTime.Now.AddDays(-i * 2),
                    Deleted = i > dataCount - 2,
                    Url = "Url " + i,
                    TagName = "Tag Name " + i
                });
                db.SaveChanges();
            }
            return new EfTagDal(db);
        }


    }
}

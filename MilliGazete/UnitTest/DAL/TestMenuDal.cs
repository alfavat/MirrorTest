using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Models;
using System;
using System.Linq;

namespace UnitTest.DAL
{
    public class TestMenuDal : TestMilliGazeteDbContext
    {
        public readonly IMenuDal menuDal;
        public TestMenuDal()
        {
            if (menuDal == null)
                menuDal = MockMenuDal();
        }

        IMenuDal MockMenuDal()
        {
            var list = db.Menus.ToList();
            db.Menus.RemoveRange(list);
            for (int i = 1; i <= dataCount; i++)
            {
                db.Menus.Add(new Menu()
                {
                    Active = i < 8,
                    Id = i,
                    CreatedAt = DateTime.Now.AddDays(-i * 2),
                    Deleted = i > dataCount - 2,
                    Url = "Url " + i,
                    Title = "title " + i,
                    ParentMenuId = null
                });
                db.SaveChanges();
            }
            return new EfMenuDal(db);

        }
    }
}

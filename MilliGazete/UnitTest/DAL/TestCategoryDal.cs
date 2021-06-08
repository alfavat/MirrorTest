using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Models;
using System;
using System.Linq;

namespace UnitTest.DAL
{
    public class TestCategoryDal : TestMilliGazeteDbContext
    {
        public readonly ICategoryDal categoryDal;
        public TestCategoryDal()
        {
            if (categoryDal == null)
                categoryDal = MockCategoryDal();
        }

        ICategoryDal MockCategoryDal()
        {
            var list = db.Category.ToList();
            db.Category.RemoveRange(list);
            for (int i = 1; i <= dataCount; i++)
            {
                db.Category.Add(new Category()
                {
                    Active = i < 8,
                    Id = i,
                    CreatedAt = DateTime.Now.AddDays(-i * 2),
                    Deleted = i > dataCount - 2,
                    Url = "Url " + i,
                    SeoKeywords = "keys " + i,
                    SeoDescription = "Seo Desc" + i,
                    CategoryName = "Category Name " + i,
                    IsStatic = i < dataCount - 3,
                    StyleCode = "Style Code " + i,
                    Symbol = "Symbol_" + i
                });
                db.SaveChanges();
            }
            return new EfCategoryDal(db);

        }
    }
}

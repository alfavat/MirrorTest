using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Models;
using System.Linq;

namespace UnitTest.DAL
{
    public class TestUserCategoryRelationDal : TestMilliGazeteDbContext
    {
        public readonly IUserCategoryRelationDal userCategoryRelationDal;
        public TestUserCategoryRelationDal()
        {
            if (userCategoryRelationDal == null)
                userCategoryRelationDal = MockUserCategoryRelationDal();
        }

        IUserCategoryRelationDal MockUserCategoryRelationDal()
        {
            var list = db.UserCategoryRelations.ToList();
            db.UserCategoryRelations.RemoveRange(list);
            for (int i = 1; i <= dataCount; i++)
            {
                db.UserCategoryRelations.Add(new UserCategoryRelation()
                {
                    Id = i,
                    UserId = i < 3 ? 1 : i,
                    CategoryId = i < 3 ? 1 : i
                });
                db.SaveChanges();
            }
            return new EfUserCategoryRelationDal(db);

        }
    }
}

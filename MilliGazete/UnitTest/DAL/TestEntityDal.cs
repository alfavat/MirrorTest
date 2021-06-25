using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System.Linq;

namespace UnitTest.DAL
{
    public class TestEntityDal : TestMilliGazeteDbContext
    {
        public readonly IEntityDal entityDal;
        public TestEntityDal()
        {
            if (entityDal == null)
                entityDal = MockEntityDal();
        }

        IEntityDal MockEntityDal()
        {
            var list = db.Entities.ToList();
            db.Entities.RemoveRange(list);
            for (int i = 1; i <= dataCount; i++)
            {
                db.Entities.Add(new Entity.Models.Entity()
                {
                    Id = i,
                    EntityGroupId = i < 3 ? 1 : i < 5 ? 2 : i,
                    EntityName = "entity_" + i
                });
                db.SaveChanges();
            }
            return new EfEntityDal(db);

        }
    }
}

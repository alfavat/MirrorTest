using DataAccess.Abstract;
using DataAccess.Base;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entity.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserCategoryRelationDal : EfEntityRepositoryBase<UserCategoryRelation>, IUserCategoryRelationDal
    {
        public EfUserCategoryRelationDal(MilliGazeteDbContext milligazeteDb) : base(milligazeteDb)
        {
        }
        public async Task AddRange(List<UserCategoryRelation> userCategoryRelations)
        {
            using (var transaction = Db.Database.BeginTransaction())
            {
                try
                {
                    var list = Db.UserCategoryRelation.Where(f => f.UserId == userCategoryRelations.FirstOrDefault().UserId);

                    if (list != null && list.Any())
                    {
                        Db.UserCategoryRelation.RemoveRange(list);
                        Db.SaveChanges();
                    }

                    Db.UserCategoryRelation.AddRange(userCategoryRelations);
                    Db.SaveChanges();
                    await transaction.CommitAsync();
                }
                catch (System.Exception ec)
                {
                    await transaction.RollbackAsync();
                    throw ec;
                }
            }
        }
    }
}

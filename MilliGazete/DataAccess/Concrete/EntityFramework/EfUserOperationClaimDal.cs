using DataAccess.Abstract;
using DataAccess.Base;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entity.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserOperationClaimDal : EfEntityRepositoryBase<UserOperationClaim>, IUserOperationClaimDal
    {
        public EfUserOperationClaimDal(MilliGazeteDbContext milligazeteDb) : base(milligazeteDb)
        {
        }
        public async Task AddRange(List<UserOperationClaim> userOperationClaims)
        {
            using (var transaction = Db.Database.BeginTransaction())
            {
                try
                {
                    var userId = userOperationClaims.Select(f => f.UserId).FirstOrDefault();
                    var userClaims = Db.UserOperationClaim.Where(f => f.UserId == userId).ToList();
                    if (userClaims != null)
                    {
                        Db.UserOperationClaim.RemoveRange(userClaims);
                        Db.SaveChanges();
                    }

                    Db.UserOperationClaim.AddRange(userOperationClaims);
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

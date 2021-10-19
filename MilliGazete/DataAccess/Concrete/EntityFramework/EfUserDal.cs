using DataAccess.Abstract;
using DataAccess.Base;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User>, IUserDal
    {
        public EfUserDal(MilliGazeteDbContext milligazeteDb) : base(milligazeteDb)
        {
        }
        public async Task AddUserClaim(UserOperationClaim claim)
        {
            Db.UserOperationClaims.Add(claim);
            await Db.SaveChangesAsync();
        }

        public async Task<User> AddWithCliams(User user, int[] defaultUserOperationClaims)
        {
            using (var transaction = Db.Database.BeginTransaction())
            {
                try
                {
                    Db.Users.Add(user);
                    await Db.SaveChangesAsync();
                    var claims = new List<UserOperationClaim>();
                    if (defaultUserOperationClaims != null && defaultUserOperationClaims.Length > 0)
                    {
                        foreach (var item in defaultUserOperationClaims)
                        {
                            claims.Add(new UserOperationClaim
                            {
                                UserId = user.Id,
                                OperationClaimId = item
                            });
                        }
                        Db.UserOperationClaims.AddRange(claims);
                    }
                    await Db.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return user;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}

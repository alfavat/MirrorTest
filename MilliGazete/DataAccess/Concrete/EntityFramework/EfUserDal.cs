using DataAccess.Abstract;
using DataAccess.Base;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entity.Models;
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
    }
}

using DataAccess.Abstract;
using DataAccess.Base;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entity.Models;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserPasswordRequestDal : EfEntityRepositoryBase<UserPasswordRequest>, IUserPasswordRequestDal
    {
        public EfUserPasswordRequestDal(MilliGazeteDbContext milligazeteDb) : base(milligazeteDb)
        {
        }
    }
}

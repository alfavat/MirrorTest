using DataAccess.Abstract;
using DataAccess.Base;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entity.Models;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfSubscriptionDal : EfEntityRepositoryBase<Subscription>, ISubscriptionDal
    {
        public EfSubscriptionDal(MilliGazeteDbContext milligazeteDb) : base(milligazeteDb)
        {
        }
    }
}

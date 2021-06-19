using DataAccess.Abstract;
using DataAccess.Base;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entity.Models;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfReporterDal : EfEntityRepositoryBase<Reporter>, IReporterDal
    {
        public EfReporterDal(MilliGazeteDbContext milligazeteDb) : base(milligazeteDb)
        {
        }
    }
}

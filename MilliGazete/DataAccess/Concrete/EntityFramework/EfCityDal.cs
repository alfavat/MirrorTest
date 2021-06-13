using DataAccess.Abstract;
using DataAccess.Base;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entity.Models;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCityDal : EfEntityRepositoryBase<City>, ICityDal
    {
        public EfCityDal(MilliGazeteDbContext milligazeteDb) : base(milligazeteDb)
        {
        }
    }
}

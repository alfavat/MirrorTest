using DataAccess.Abstract;
using DataAccess.Base;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entity.Models;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfNewspaperDal : EfEntityRepositoryBase<Newspaper>, INewspaperDal
    {
        public EfNewspaperDal(MilliGazeteDbContext milligazeteDb) : base(milligazeteDb)
        {
        }
    }
}

using DataAccess.Abstract;
using DataAccess.Base;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entity.Models;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfNewsRelatedNewsDal : EfEntityRepositoryBase<NewsRelatedNews>, INewsRelatedNewsDal
    {
        public EfNewsRelatedNewsDal(MilliGazeteDbContext milligazeteDb) : base(milligazeteDb)
        {
        }
    }
}

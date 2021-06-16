using DataAccess.Abstract;
using DataAccess.Base;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entity.Models;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfQuestionDal : EfEntityRepositoryBase<Question>, IQuestionDal
    {
        public EfQuestionDal(MilliGazeteDbContext milligazeteDb) : base(milligazeteDb)
        {
        }
    }
}

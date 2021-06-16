using DataAccess.Abstract;
using DataAccess.Base;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entity.Models;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfQuestionAnswerDal : EfEntityRepositoryBase<QuestionAnswer>, IQuestionAnswerDal
    {
        public EfQuestionAnswerDal(MilliGazeteDbContext milligazeteDb) : base(milligazeteDb)
        {
        }
    }
}

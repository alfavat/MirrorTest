using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Models;
using System;
using System.Linq;

namespace UnitTest.DAL
{
    public class TestQuestionAnswerDal : TestMilliGazeteDbContext
    {
        public readonly IQuestionAnswerDal questionAnswerDal;
        public TestQuestionAnswerDal()
        {
            if (questionAnswerDal == null)
                questionAnswerDal = MockQuestionAnswerDal();
        }

        IQuestionAnswerDal MockQuestionAnswerDal()
        {
            var list = db.QuestionAnswers.ToList();
            db.QuestionAnswers.RemoveRange(list);
            for (int i = 1; i <= dataCount; i++)
            {
                db.QuestionAnswers.Add(new QuestionAnswer()
                {
                    Id = i,
                    Deleted = i > dataCount - 2,
                    Answer = "Answer " + i.ToString(),
                    QuestionId = 1
                });
                db.SaveChanges();
            }
            return new EfQuestionAnswerDal(db);

        }
    }
}

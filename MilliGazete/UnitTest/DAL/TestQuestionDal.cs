using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Models;
using System;
using System.Linq;

namespace UnitTest.DAL
{
    public class TestQuestionDal : TestMilliGazeteDbContext
    {
        public readonly IQuestionDal questionDal;
        public TestQuestionDal()
        {
            if (questionDal == null)
                questionDal = MockQuestionDal();
        }

        IQuestionDal MockQuestionDal()
        {
            var list = db.Questions.ToList();
            db.Questions.RemoveRange(list);
            for (int i = 1; i <= dataCount; i++)
            {
                db.Questions.Add(new Question()
                {
                    Id = i,
                    CreatedAt = DateTime.Now.AddDays(-i * 2),
                    Deleted = i > dataCount - 2,
                    Active = true,
                    QuestionText = "Question " + i.ToString()
                });
                db.SaveChanges();
            }
            return new EfQuestionDal(db);

        }
    }
}

using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Models;
using System;
using System.Linq;

namespace UnitTest.DAL
{
    public class TestUserQuestionAnswerDal : TestMilliGazeteDbContext
    {
        public readonly IUserQuestionAnswerDal userQuestionAnswerDal;
        public TestUserQuestionAnswerDal()
        {
            if (userQuestionAnswerDal == null)
                userQuestionAnswerDal = MockUserQuestionAnswerDal();
        }

        IUserQuestionAnswerDal MockUserQuestionAnswerDal()
        {
            var list = db.UserQuestionAnswers.ToList();
            db.UserQuestionAnswers.RemoveRange(list);
            for (int i = 1; i <= dataCount; i++)
            {
                db.UserQuestionAnswers.Add(new UserQuestionAnswer()
                {
                    Id = i,
                    CreatedAt = DateTime.Now.AddDays(-i * 2),
                    AnswerId = 1,
                    IpAddress = "192.168.1." + i.ToString(),
                    QuestionId = 1
                });
                db.SaveChanges();
            }
            return new EfUserQuestionAnswerDal(db);

        }
    }
}

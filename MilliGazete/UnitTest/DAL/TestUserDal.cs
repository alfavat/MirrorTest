using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Models;
using System;
using System.Linq;

namespace UnitTest.DAL
{
    public class TestUserDal : TestMilliGazeteDbContext
    {
        public IUserDal userDal;
        public TestUserDal()
        {
            if (userDal == null)
                userDal = MockUserDal();
        }

        IUserDal MockUserDal()
        {
            var list = db.User.ToList();
            db.User.RemoveRange(list);
            for (int i = 1; i <= dataCount; i++)
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash("Password_" + i, out passwordHash, out passwordSalt);
                db.User.Add(new User()
                {
                    Active = i < 8,
                    Id = i,
                    CreatedAt = DateTime.Now.AddDays(-i * 2),
                    Deleted = i > dataCount - 2,
                    Email = "test" + i + "@test.com",
                    FirstName = "Fname " + i,
                    LastName = "Lname " + i,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    UserName = "User_" + i,
                    LastLoginDate = DateTime.Now.AddDays(-i),
                    LastLoginIpAddress = "192.168.1." + i
                });
                db.SaveChanges();
            }
            return new EfUserDal(db);
        }
    }
}

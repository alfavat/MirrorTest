using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Models;
using System.Linq;

namespace UnitTest.DAL
{
    public class TestUserOperationClaimDal : TestMilliGazeteDbContext
    {
        public IUserOperationClaimDal userOperationClaimDal;
        public TestUserOperationClaimDal()
        {
            if (userOperationClaimDal == null)
                userOperationClaimDal = MockUserOperationClaimDal();
        }

        IUserOperationClaimDal MockUserOperationClaimDal()
        {
            var list = db.UserOperationClaims.ToList();
            db.UserOperationClaims.RemoveRange(list);
            for (int i = 1; i <= dataCount; i++)
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash("Password_" + i, out passwordHash, out passwordSalt);
                db.UserOperationClaims.Add(new UserOperationClaim()
                {
                    Id = i,
                    UserId = i % 2 == 0 ? 1 : i,
                    OperationClaimId = i % 2 == 0 ? 1 : i
                });
                db.SaveChanges();
            }
            return new EfUserOperationClaimDal(db);
        }
    }
}

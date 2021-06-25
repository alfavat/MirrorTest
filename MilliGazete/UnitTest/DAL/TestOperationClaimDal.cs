using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Models;
using System.Linq;

namespace UnitTest.DAL
{
    public class TestOperationClaimDal : TestMilliGazeteDbContext
    {
        public IOperationClaimDal operationClaimDal;
        public TestOperationClaimDal()
        {
            if (operationClaimDal == null)
                operationClaimDal = MockOperationClaimDal();
        }

        IOperationClaimDal MockOperationClaimDal()
        {
            var list = db.OperationClaims.ToList();
            db.OperationClaims.RemoveRange(list);
            for (int i = 1; i <= dataCount; i++)
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash("Password_" + i, out passwordHash, out passwordSalt);
                db.OperationClaims.Add(new OperationClaim()
                {
                    Id = i,
                    ClaimName = "claim " + i
                });
                db.SaveChanges();
            }
            return new EfOperationClaimDal(db);
        }
    }
}

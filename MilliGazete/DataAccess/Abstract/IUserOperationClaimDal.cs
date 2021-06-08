using DataAccess.Base;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserOperationClaimDal : IEntityRepository<UserOperationClaim>
    {
        Task AddRange(List<UserOperationClaim> userOperationClaims);
    }
}

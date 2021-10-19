using DataAccess.Base;
using Entity.Models;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        Task AddUserClaim(UserOperationClaim claim);
        Task<User> AddWithCliams(User user, int[] defaultUserOperationClaims);
    }
}

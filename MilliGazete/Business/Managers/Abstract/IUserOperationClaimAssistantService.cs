using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IUserOperationClaimAssistantService
    {
        Task<List<UserOperationClaimViewDto>> GetByUserId(int userId);
        Task<UserOperationClaim> GetById(int id);
        Task Update(UserOperationClaimUpdateDto userOperationClaim);
        Task Add(UserOperationClaim userOperationClaim);
        Task<List<UserOperationClaim>> GetClaimsByUserId(int userId);
    }
}

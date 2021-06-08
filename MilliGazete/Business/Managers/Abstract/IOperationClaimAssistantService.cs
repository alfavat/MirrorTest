using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IOperationClaimAssistantService
    {
        Task<List<OperationClaimDto>> GetList();
    }
}

using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IUserOperationClaimService
    {
        Task<IDataResult<List<UserOperationClaimViewDto>>> GetByUserId(int userId);
        Task<IResult> Update(UserOperationClaimUpdateDto dto);
    }
}

using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IUserCategoryRelationService
    {
        Task<IResult> Update(UserCategoryRelationUpdateDto userCategoryRelationUpdateDto);
        Task<IDataResult<List<UserCategoryRelationViewDto>>> GetListByUserId(int userId);
    }
}

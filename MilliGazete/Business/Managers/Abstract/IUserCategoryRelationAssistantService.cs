using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IUserCategoryRelationAssistantService
    {
        Task Update(UserCategoryRelationUpdateDto dto);
        Task<List<UserCategoryRelationViewDto>> GetByUserId(int userId);
    }
}

using Entity.Dtos;
using Entity.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IEntityAssistantService
    {
        Task<List<EntityDto>> GetByGroupId(EntityGroupType entityGroupType);
    }
}

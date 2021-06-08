using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IEntityService
    {
        Task<IDataResult<List<EntityDto>>> GetNewsTypeEntities();
        Task<IDataResult<List<EntityDto>>> GetNewsAgencyEntities();
        Task<IDataResult<List<EntityDto>>> GetNewsFileTypeEntities();
        Task<IDataResult<List<EntityDto>>> GetPropertyEntities();
        Task<IDataResult<List<EntityDto>>> GetPositionEntities();
        Task<IDataResult<List<EntityDto>>> GetCounterEntities();

    }
}

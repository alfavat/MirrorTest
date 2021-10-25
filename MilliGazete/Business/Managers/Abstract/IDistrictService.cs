using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IDistrictService
    {
        Task<IDataResult<List<DistrictDto>>> GetList();
        Task<IDataResult<List<DistrictDto>>> GetListByCityId(int id);
    }
}

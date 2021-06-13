using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface ICityService
    {
        Task<IDataResult<List<CityDto>>> GetList();
    }
}

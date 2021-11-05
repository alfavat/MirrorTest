using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Repository.Abstract
{
    public interface ICityRepository : IUIBaseRepository
    {
        Task<IDataResult<List<CityDto>>> GetList();
    }
}

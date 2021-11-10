using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Repository.Abstract;

namespace WebUI.Repository.Concrete
{
    public class CityRepository : ICityRepository
    {
        public async Task<IDataResult<List<CityDto>>> GetList()
        {
            return await ApiHelper.GetApiAsync<List<CityDto>>("Cities/getlist");
        }
    }
}

using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IDistrictAssistantService
    {
        Task<List<DistrictDto>> GetListByCityId(int id);
        Task<List<DistrictDto>> GetList();
    }
}

using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IDistrictAssistantService
    {
        Task<List<DistrictDto>> GetListByCityId(int id);
    }
}

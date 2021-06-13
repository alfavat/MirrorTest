using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface ICityAssistantService
    {
        Task<List<CityDto>> GetList();
    }
}

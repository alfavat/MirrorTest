using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IAdvertisementAssistantService
    {
        Task<Advertisement> GetById(int id);
        Task Update(Advertisement advertisement);
        Task Delete(Advertisement advertisement);
        Task<List<AdvertisementDto>> GetList();
        Task Add(Advertisement Advertisement);
        Task<List<AdvertisementDto>> GetActiveList();
    }
}

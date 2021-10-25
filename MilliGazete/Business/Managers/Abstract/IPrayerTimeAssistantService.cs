using Entity.Dtos;
using Entity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IPrayerTimeAssistantService
    {
        Task<List<PrayerTimeDto>> GetList();
        Task Add(PrayerTime item);
        Task Update(PrayerTime item);
        Task<List<PrayerTimeDto>> GetPrayerTimeByCityId(int cityId);
    }
}

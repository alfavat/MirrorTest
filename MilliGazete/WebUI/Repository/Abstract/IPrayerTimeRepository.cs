using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Repository.Abstract
{
    public interface IPrayerTimeRepository : IUIBaseRepository
    {
        Task<IDataResult<List<PrayerTimeDto>>> GetPrayerTime(int cityId);
    }
}

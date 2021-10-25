using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IPrayerTimeService
    {
        Task<IResult> AddArray(List<PrayerTimeAddDto> arr);
        Task<IDataResult<List<PrayerTimeDto>>> GetList();
        Task<IDataResult<List<PrayerTimeDto>>> GetPrayerTimeByCityId(int cityId);
    }
}

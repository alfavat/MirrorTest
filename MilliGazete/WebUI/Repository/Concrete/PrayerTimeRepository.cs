using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Repository.Abstract;

namespace WebUI.Repository.Concrete
{
    public class PrayerTimeRepository : IPrayerTimeRepository
    {
        public async Task<IDataResult<List<PrayerTimeDto>>> GetPrayerTime(int cityId = 34)
        {
            string param = "?cityId=" + cityId;
            return await ApiHelper.GetApiAsync<List<PrayerTimeDto>>("PrayerTimes/getlistbycityid" + param);
        }
    }
}

using Entity.Dtos;
using ServerService.Helper;
using System;
using System.Collections.Generic;
using static ServerService.Helper.HttpHelper;

namespace ServerService.Helpers
{
    public class PrayerTimeHelper
    {
        public static bool AddArrayPrayerTimes(List<PrayerTimeAddDto> listPrayerTimes)
        {
            if (!listPrayerTimes.HasValue())
            {
                throw new Exception("no data to send to web api");
            }
            using (HttpHelper http = new HttpHelper())
            {
                try
                {
                    var cityAndDistrictsSave = http.Request<dynamic>(new RequestObject()
                    {
                        Url = AppSettingsHelper.ApiLink + "prayertimes/addarray",
                        AuthorizationHeaderValue = "bearer " + AccessToken.Token,
                        Method = "POST",
                        Body = listPrayerTimes
                    });
                    return true;
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}

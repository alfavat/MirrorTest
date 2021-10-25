using Core.Utilities.Results;
using Entity.Dtos;
using Newtonsoft.Json;
using ServerService.Helper;
using System.Collections.Generic;
using static ServerService.Helper.HttpHelper;

namespace ServerService.Helpers
{
    public class CityHelper
    {
        public static List<CityDto> GetCityList()
        {
            using (var http = new HttpHelper())
            {
                var list = http.Request<dynamic>(new RequestObject()
                {
                    Url = AppSettingsHelper.ApiLink + "cities/getlist",
                    AuthorizationHeaderValue = "bearer " + AccessToken.Token,
                    Method = "GET"
                });
                var res = JsonConvert.DeserializeObject<List<CityDto>>(list.data.ToString());
                return res;
            }
        }

    }
}
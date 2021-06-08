using System.Collections.Generic;
using static ServerService.Helper.HttpHelper;

namespace ServerService.Helper
{
    public class ForeksHelper
    {
        public static List<T> GetData<T>(string link)
        {
            using (HttpHelper http = new HttpHelper())
            {
                List<T> response = http.Request<List<T>>(new RequestObject()
                {
                    Url = link,
                    AuthorizationHeaderValue = "Basic " + AppSettingsHelper.ForeksAuthorizationToken,
                    Method = "GET"
                });
                return response;
            }
        }
    }
}

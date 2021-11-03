using Entity.Dtos;
using ServerService.Helper;
using System;
using System.Collections.Generic;
using static ServerService.Helper.HttpHelper;

namespace ServerService.Helpers
{
    public class NewsPaperHelper
    {
        public static bool AddArrayNewsPapers(List<NewspaperAddDto> listNewsPapers)
        {
            if (!listNewsPapers.HasValue())
            {
                throw new Exception("no data to send to web api");
            }
            using (HttpHelper http = new HttpHelper())
            {
                try
                {
                    var res = http.Request<dynamic>(new RequestObject()
                    {
                        Url = AppSettingsHelper.ApiLink + "Newspapers/addarray",
                        AuthorizationHeaderValue = "bearer " + AccessToken.Token,
                        Method = "POST",
                        Body = listNewsPapers
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

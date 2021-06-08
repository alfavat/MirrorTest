using System;
using static ServerService.Helper.HttpHelper;

namespace ServerService.Helper
{
    public class AccessToken
    {
        public static string Token { get; set; }
        public static DateTime Expiration { get; set; }
        public static string RefreshToken { get; set; }
    }

    public static class ApiHelper
    {
        public static void GetToken()
        {
            using (HttpHelper http = new HttpHelper())
            {
                var tokenRequest = http.Request<dynamic>(new RequestObject()
                {
                    Url = AppSettingsHelper.ApiLink + "auth/login",
                    Method = "POST",
                    Body = new
                    {
                        emailOrUserName = AppSettingsHelper.ApiUserName,
                        password = AppSettingsHelper.ApiPassword
                    }
                });

                AccessToken.Token = tokenRequest.data.token;
            }
        }
    }
}

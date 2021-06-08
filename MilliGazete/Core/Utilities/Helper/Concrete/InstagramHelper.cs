using Core.Utilities.Helper.Abstract;
using Microsoft.Extensions.Configuration;

namespace Core.Utilities.Helper
{
    public class InstagramHelper : IInstagramHelper
    {
        public IConfiguration _configuration { get; }
        public InstagramHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public dynamic RequestLongLivedAccessToken(string accessToken)
        {
            var instagramAppSetting  = _configuration.GetSection("InstagramAppSettings").Get<InstagramAppSetting>();
            var URL = "https://graph.facebook.com/v5.0/oauth/access_token?grant_type=fb_exchange_token&client_id=" + instagramAppSetting.AppId
                + "&client_secret=" + instagramAppSetting.SecretId + "&fb_exchange_token=" + accessToken;

            return new HttpHelper().Request<dynamic>(new HttpHelper.RequestObject()
            {
                Url = URL,
                Method = "GET"
            });
        }
        public class InstagramAppSetting
        {
            public string AppId { get; set; }
            public string SecretId { get; set; }
        }
    }
}

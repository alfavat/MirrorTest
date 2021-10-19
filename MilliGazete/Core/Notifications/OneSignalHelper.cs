using Core.Utilities.Results;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OneSignal.RestAPIv3.Client;
using OneSignal.RestAPIv3.Client.Resources.Notifications;
using System;
using System.Threading.Tasks;

namespace Core.Notifications
{
    public class OneSignalSettings
    {
        public string AppId { get; set; }
        public string ApiKey { get; set; }
    }
    public class OneSignalHelper : IPushNotificationHelper
    {
        public IConfiguration _configuration { get; }
        public OneSignalHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IResult> SendNotification(NotificationCreateOptions options)
        {
            try
            {
                var appSettings = _configuration.GetSection("OneSignalSettings").Get<OneSignalSettings>();
                var client = new OneSignalClient(appSettings.ApiKey);
                options.AppId = new Guid(appSettings.AppId);
                options.Priority = 10;
                var result = await client.Notifications.CreateAsync(options);
                return new SuccessResult(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message + (ex.InnerException == null ? "" : ex.InnerException.Message));
            }
        }
    }
}

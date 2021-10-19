using Business.Managers.Abstract;
using Core.Notifications;
using Core.Utilities.Results;
using Entity.Dtos;
using Microsoft.Extensions.Configuration;
using OneSignal.RestAPIv3.Client.Resources;
using OneSignal.RestAPIv3.Client.Resources.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class OneSignalPushNotificationManager : IPushNotificationService
    {
        private readonly IPushNotificationHelper _pushNotificationHelper;
        private readonly IConfiguration _configuration;

        public OneSignalPushNotificationManager(IPushNotificationHelper pushNotificationHelper, IConfiguration configuration)
        {
            _pushNotificationHelper = pushNotificationHelper;
            _configuration = configuration;
        }

        List<INotificationFilter> GetNotificationFilters(string platform)
        {
            return new List<INotificationFilter>()
                {
                   new NotificationFilterField() { Field = NotificationFilterFieldTypeEnum.Tag, Key = "platform", Relation = "=", Value = platform },
                };
        }

        public async Task<IResult> NewsAdded(NewsViewDto dto)
        {
            var options = new NotificationCreateOptions
            {
                /*Filters = new List<INotificationFilter>()
                {
                new NotificationFilterField() { Field = NotificationFilterFieldTypeEnum.Tag, Key = "companyId", Relation = "=", Value = dto.CompanyId.ToString() },
                new NotificationFilterField() { Field = NotificationFilterFieldTypeEnum.Tag, Key = "isCompany", Relation = "=", Value = "true" },
                new NotificationFilterOperator() { Operator = "OR" },
                 new NotificationFilterField() { Field = NotificationFilterFieldTypeEnum.Tag, Key = "userId", Relation = "=", Value = dto.TransporterUserId.ToString() },
                new NotificationFilterField() { Field = NotificationFilterFieldTypeEnum.Tag, Key = "isTransporter", Relation = "=", Value = "true" },
                   new NotificationFilterField() { Field = NotificationFilterFieldTypeEnum.Tag, Key = "loginStatus", Relation = "=", Value = "2" },
                },
                Filters = GetNotificationFilters("web"),*/
                Data = new Dictionary<string, string>(),
                IncludedSegments = new List<string>()
            };

            options.IncludedSegments.Add("Subscribed Users");

            options.ActionButtons = new List<ActionButtonField>() { new ActionButtonField() { Text = "Detaylar", Id = "btnViewNews" } };
            options.WebButtons = new List<WebButtonField>() { new WebButtonField() { Text = "Detaylar", Id = "btnViewNews", Url = _configuration.GetSection("WebUIUrl").Get<string>() + dto.Url } };

            options.Data.Add("url", dto.Url);
            options.Data.Add("historyNo", dto.HistoryNo.ToString());
            options.Data.Add("newsId", dto.Id.ToString());
            options.Data.Add("type", "newsAdded");

            options.Headings.Add(LanguageCodes.English, "MilliGazete");
            options.Contents.Add(LanguageCodes.English, dto.Title);
            return await _pushNotificationHelper.SendNotification(options);
        }

        public async Task<IResult> TestNewsAdded(NewsViewDto dto, string segment)
        {
            var options = new NotificationCreateOptions
            {
                Data = new Dictionary<string, string>(),
                IncludedSegments = new List<string>()
            };

            options.IncludedSegments.Add(segment);

            options.ActionButtons = new List<ActionButtonField>() { new ActionButtonField() { Text = "Detaylar", Id = "btnViewNews" } };
            options.WebButtons = new List<WebButtonField>() { new WebButtonField() { Text = "Detaylar", Id = "btnViewNews", Url = _configuration.GetSection("WebUIUrl").Get<string>() + dto.Url } };

            options.Data.Add("url", dto.Url);
            options.Data.Add("historyNo", dto.HistoryNo.ToString());
            options.Data.Add("newsId", dto.Id.ToString());
            options.Data.Add("type", "newsAdded");

            options.Headings.Add(LanguageCodes.English, "MilliGazete");
            options.Contents.Add(LanguageCodes.English, dto.Title);
            return await _pushNotificationHelper.SendNotification(options);
        }
    }
}

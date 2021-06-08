using Business.Managers.Abstract;
using Core.Notifications;
namespace Business.Managers.Concrete
{
    public class OneSignalPushNotificationManager : IPushNotificationService
    {
        private readonly IPushNotificationHelper _pushNotificationHelper;
        private readonly IPushNotificationAssistantService _pushNotificationAssistantService;
        public OneSignalPushNotificationManager(IPushNotificationHelper pushNotificationHelper, IPushNotificationAssistantService pushNotificationAssistantService)
        {
            _pushNotificationHelper = pushNotificationHelper;
            _pushNotificationAssistantService = pushNotificationAssistantService;
        }

        //public IResult NewOrder(Order order)
        //{
        //    var options = new NotificationCreateOptions
        //    {
        //        Filters = new List<INotificationFilter>()
        //        {
        //            new NotificationFilterField() { Field = NotificationFilterFieldTypeEnum.Tag, Key = "companyId", Relation = "=", Value = order.CompanyId.ToString() },
        //            new NotificationFilterField() { Field = NotificationFilterFieldTypeEnum.Tag, Key = "isCompany", Relation = "=", Value = "true" },
        //            new NotificationFilterOperator() { Operator = "OR" },
        //            new NotificationFilterField() { Field = NotificationFilterFieldTypeEnum.Tag, Key = "userId", Relation = "=", Value = order.TransporterUserId.ToString() },
        //            new NotificationFilterField() { Field = NotificationFilterFieldTypeEnum.Tag, Key = "isTransporter", Relation = "=", Value = "true" },
        //        },
        //        Url = PageLinks.LiveOrderListLink,
        //        Data = new Dictionary<string, string>(),
        //    };
        //    options.Data.Add("orderId", order.Id.ToString());
        //    options.Data.Add("type", "newOrder");

        //    options.Headings.Add(LanguageCodes.English, PushNotificationMessages.NewOrderHeading);
        //    options.Contents.Add(LanguageCodes.English, string.Format(PushNotificationMessages.NewOrderContent, order.Total));
        //    return _pushNotificationHelper.SendNotification(options);
        //}
    }
}

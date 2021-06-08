using Core.Utilities.Results;
using OneSignal.RestAPIv3.Client.Resources.Notifications;

namespace Core.Notifications
{
    public interface IPushNotificationHelper
    {
        IResult SendNotification(NotificationCreateOptions options);
    }
}

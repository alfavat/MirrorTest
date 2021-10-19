using Core.Utilities.Results;
using OneSignal.RestAPIv3.Client.Resources.Notifications;
using System.Threading.Tasks;

namespace Core.Notifications
{
    public interface IPushNotificationHelper
    {
        Task<IResult> SendNotification(NotificationCreateOptions options);
    }
}

using Business.Helpers.Concrete;

namespace Business.Constants
{
    public static class PushNotificationMessages
    {
        public static string NewOrderHeading { get => Translator.GetByKey("newOrderHeading"); }
    }
}

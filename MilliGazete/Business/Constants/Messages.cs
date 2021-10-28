using Business.Helpers.Concrete;

namespace Business.Constants
{
    public class Messages
    {
        public static string UserNotFound { get => Translator.GetByKey("messageUserNotFound"); }
        public static string UserIsNotActive { get => Translator.GetByKey("messageUserIsNotActive"); }
        public static string PasswordError { get => Translator.GetByKey("messagePasswordError"); }
        public static string SuccessForLogin { get => Translator.GetByKey("messageSuccessForLogin"); }
        public static string UserAlreadyExists { get => Translator.GetByKey("messageUserAlreadyExists"); }
        public static string UserNameAlreadyExists { get => Translator.GetByKey("messageUserNameAlreadyExists"); }
        public static string EmailAlreadyExists { get => Translator.GetByKey("messageEmailAlreadyExists"); }
        public static string UserRegistered { get => Translator.GetByKey("messageUserRegistered"); }
        public static string AcccessTokenCreated { get => Translator.GetByKey("messageAcccessTokenCreated"); }
        public static string AuthorizationDenied { get => Translator.GetByKey("messageAuthorizationDenied"); }
        public static string AuthenticationDenied { get => Translator.GetByKey("messageAuthenticationDenied"); }
        public static string RecordNotFound { get => Translator.GetByKey("messageRecordNotFound"); }
        public static string Added { get => Translator.GetByKey("messageAdded"); }
        public static string Deleted { get => Translator.GetByKey("messageDeleted"); }
        public static string Updated { get => Translator.GetByKey("messageUpdated"); }
        public static string FileUploadError { get => Translator.GetByKey("messageFileUploadError"); }
        public static string FileUploadSuccess { get => Translator.GetByKey("messageFileUploadSuccess"); }
        public static string FileSizeLimitError { get => Translator.GetByKey("messageFileSizeLimitError"); }
        public static string FileTypeError { get => Translator.GetByKey("messageFileTypeError"); }
        public static string EmptyToken { get => Translator.GetByKey("messageEmptyToken"); }
        public static string MailError { get => Translator.GetByKey("messageEmailError"); }
        public static string EmailNotFound { get => Translator.GetByKey("messageEmailNotFound"); }
        public static string ExpirationDatePassed { get => Translator.GetByKey("messageExpirationDatePassed"); }
        public static string NoRecordFound { get => Translator.GetByKey("messageNoRecordFound"); }
        public static string MailSent { get => Translator.GetByKey("messageMailSent"); }
        public static string EmptyParameter { get => Translator.GetByKey("messageEmptyParameter"); }
        public static string RecordAlreadyExists { get => Translator.GetByKey("messageRecordAlreadyExists"); }
        public static string NameSurenameAlreadyExists { get => Translator.GetByKey("messageNameSurenameAlreadyExists"); }
        public static string UrlAlreadyExists { get => Translator.GetByKey("messageUrlAlreadyExists"); }
        public static string PushNotificationActiveDraftError { get => Translator.GetByKey("messagePushNotificationActiveDraftError"); }
        public static string PushNotificationPublishDateError { get => Translator.GetByKey("messagePushNotificationPublishDateError"); }
        public static string FlashNewsActiveDraftError { get => Translator.GetByKey("messageFlashNewsActiveDraftError"); }
        public static string FlashNewsPublishDateError { get => Translator.GetByKey("messageFlashNewsPublishDateError"); }
    }
}

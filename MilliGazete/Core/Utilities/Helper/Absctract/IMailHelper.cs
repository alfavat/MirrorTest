namespace Core.Utilities.Helper.Abstract
{
    public interface IMailHelper
    {
        bool SendEmail(string subject,string body, string[] to);
    }
}

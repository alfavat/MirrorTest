namespace Core.Utilities.Helper.Abstract
{
    public interface IDownloadHelper
    {
        string DownloadImage(string imageUrl, string imageName, string destinationPath);
    }
}

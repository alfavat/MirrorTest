using Core.Utilities.Helper.Abstract;
using MimeTypes;
using System;
using System.IO;
using System.Net;

namespace Core.Utilities.Helper.Concrete
{
    public class DownloadHelper : IDownloadHelper
    {
        public string DownloadImage(string imageUrl, string imageName, string destinationPath)
        {
            try
            {
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), destinationPath);
                using (WebClient webClient = new WebClient())
                {
                    if (!Directory.Exists(pathToSave))
                    {
                        Directory.CreateDirectory(pathToSave);
                    }
                    var result = webClient.DownloadData(imageUrl);
                    var contentType = webClient.ResponseHeaders["Content-Type"];
                    var contentLength = webClient.ResponseHeaders["Content-Length"];

                    if (string.IsNullOrEmpty(contentType)) return string.Empty;
                    if (string.IsNullOrEmpty(contentLength)) return string.Empty;
                    var extension = MimeTypeMap.GetExtension(contentType);
                    if (string.IsNullOrEmpty(extension)) return string.Empty;
                    if (CheckIfFileSizeSmall(contentLength) || CheckIfFileSizeTooBig(contentLength)) return string.Empty;

                    var fullPath = Path.Combine(pathToSave, imageName + extension);
                    System.IO.File.WriteAllBytes(fullPath, result);
                    return Path.Combine(destinationPath, imageName + extension);
                }
            }
            catch
            {
                return string.Empty;
            }
           
        }

        public bool CheckIfFileSizeSmall(string fileSize)
        {
            long.TryParse(fileSize, out long result);
            long fileSizeKb = result / 1024;
            return fileSizeKb < 4;
        }

        public bool CheckIfFileSizeTooBig(string fileSize)
        {
            long.TryParse(fileSize, out long result);
            long fileSizeKb = result / 1024;
            return fileSizeKb > 750;
        }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Drawing;

namespace Core.Utilities.Helper.Abstract
{
    public interface IUploadHelper : IDisposable
    {
        string UploadFile(string fileNamePreWord, IFormFile file);
        string CopyFileToDownloadsFolder(string fileNamePreWord, string sourcePath, out double fileSizeKb);
        bool DeleteFile(string fileName);
        bool FileTypeControl(string fileType);
        bool FileSizeControl(string fileType, long fileSize);
        bool IsVideo(string fileType);
        Image RotateImage(Image originalImage, out bool changed);
        string DownloadNewsImage(string imageUrl);
        bool IsImage(string link);
        string DownloadNewsVideo(string videoUrl);
        string UploadFileBase64(string fileNamePreWord, string base64, string fileType);
    }
}

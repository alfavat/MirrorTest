using Core.Utilities.Helper.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;

namespace Core.Utilities.File
{
    public class UploadHelper : IUploadHelper
    {
        public static readonly string[] ImageTypes =
        {
            "image/jpeg",
            "image/png",
            "image/gif",
            "image/jpg",
            "image/bmp",
            "image/bitmap",
            "image/webp"
        };
        public static readonly string[] VideoTypes =
        {
            "video/mp4",
            "video/m3u8",
            "video/rmvb",
            "video/avi",
            "video/swf",
            "video/3gp",
            "video/mkv",
            "video/flv",
            "video/mov",
            "video/quicktime"
        };
        public static readonly string[] AudioTypes =
        {
            "audio/mp3",
            "audio/wav",
            "audio/wma",
            "audio/ogg",
            "audio/aac",
            "audio/flac",
        };
        public static readonly string[] DocumentTypes =
        {
            "document/doc",
            "document/txt",
            "document/docx",
            "document/pages",
            "document/epub",
            "document/pdf",
            "document/numbers",
            "document/csv",
            "document/xls",
            "document/xlsx",
            "document/keynote",
            "document/ppt",
            "document/pptx",
        };

        public string DownloadNewspaperImage(string imageUrl, string title , string newspaperDate)
        {
            using (WebClient client = new WebClient())
            {
                using (Stream stream = client.OpenRead(imageUrl))
                {
                    Bitmap bitmap = new Bitmap(stream);
                    var dt = DateTime.Parse(newspaperDate, new CultureInfo("tr-TR"));
                    var todayFolder = dt.Year + "-" + dt.Month + "-" + dt.Day;
                    var folderName = Path.Combine("Resources", "Newspapers", todayFolder);
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if (!Directory.Exists(pathToSave))
                    {
                        Directory.CreateDirectory(pathToSave);
                    }
                    var newFilename = Path.Combine(pathToSave, title + ".jpg");
                    if (bitmap != null)
                    {
                        bitmap.Save(newFilename, ImageFormat.Jpeg);
                    }
                    return "Resources/Newspapers/" + todayFolder + "/" + title + ".jpg";
                }
            }
        }

        public string UploadFile(string fileNamePreWord, IFormFile file)
        {
            var folderName = Path.Combine("Resources", "Downloads");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }
            if (file.Length > 0)
            {
                var guid = Guid.NewGuid().ToString().Substring(0, 4);
                var extension = Path.GetExtension(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'));
                var dateTime = $@"{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}_{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}_{DateTime.Now.Millisecond}";
                var fileName = $@"milligazete_{file.ContentType.GetFileContentType().Split('/')[0]}_{fileNamePreWord}_{dateTime}_{guid}{extension}";
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = folderName.Replace(@"\", "/") + @"/" + fileName;

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    stream.Flush();
                    stream.Dispose();
                }
                if (!IsVideo(file.ContentType) && !file.ContentType.Contains("image/webp"))
                {
                    var img = Image.FromFile(fullPath);
                    bool changed;
                    img = RotateImage(img, out changed);
                    if (changed)
                        img.Save(fullPath);
                    img.Dispose();
                }
                return dbPath;
            }
            else
            {
                return null;
            }
        }

        public string CopyFileToDownloadsFolder(string fileNamePreWord, string sourcePath, out double fileSizeKb)
        {
            var folderName = Path.Combine("Resources", "Downloads");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }
            var guid = Guid.NewGuid().ToString().Substring(0, 4);
            var extension = Path.GetExtension(sourcePath.Trim('"'));
            var dateTime = $@"{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}_{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}_{DateTime.Now.Millisecond}";
            var fileName = $@"milligazete_image_{fileNamePreWord}_{dateTime}_{guid}{extension}";
            var fullPath = Path.Combine(pathToSave, fileName);
            var dbPath = folderName.Replace(@"\", "/") + @"/" + fileName;

            System.IO.File.Copy(sourcePath, dbPath);

            fileSizeKb = new FileInfo(fullPath).Length;

            Image img = Image.FromFile(fullPath);
            bool changed;
            img = RotateImage(img, out changed);

            if (changed)
                img.Save(fullPath);
            img.Dispose();

            return dbPath;
        }


        public string DownloadNewsImage(string imageUrl)
        {
            string guid = Guid.NewGuid().ToString();
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(imageUrl);
            Bitmap bitmap = new Bitmap(stream);
            var folderName = Path.Combine("Resources", "News");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }
            var newFilename = Path.Combine(pathToSave, guid + ".jpg");
            if (bitmap != null)
            {
                bitmap.Save(newFilename, ImageFormat.Jpeg);
            }
            stream.Flush();
            stream.Close();
            client.Dispose();
            return "Resources/News/" + guid + ".jpg";
        }

        public string DownloadNewsVideo(string videoUrl)
        {
            using (var client = new WebClient())
            {
                string guid = Guid.NewGuid().ToString();
                var folderName = Path.Combine("Resources", "News");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }
                var newFilename = Path.Combine(pathToSave, guid + ".mp4");
                client.DownloadFile(videoUrl, newFilename);
                return "Resources/News/" + guid + ".mp4";
            }
        }

        public bool DeleteFile(string fileName)
        {
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            if (System.IO.File.Exists(pathToSave))
            {
                try
                {
                    System.IO.File.Delete(fileName);
                }
                catch (Exception)
                {
                }
                return true;
            }
            return false;
        }

        public bool FileTypeControl(string fileType)
        {
            return ImageTypes.Contains(fileType) || VideoTypes.Contains(fileType) || AudioTypes.Contains(fileType) || DocumentTypes.Contains(fileType);
        }

        public bool FileSizeControl(string fileType, long fileSize)
        {
            long fileSizeKb = fileSize / 1024;
            if (ImageTypes.Contains(fileType) && fileSizeKb <= 4000) return true;
            if (VideoTypes.Contains(fileType) && fileSizeKb <= 500000) return true;
            if (AudioTypes.Contains(fileType) && fileSizeKb <= 10000) return true;
            if (DocumentTypes.Contains(fileType) && fileSizeKb <= 20000) return true;
            return false;
        }
        public bool IsVideo(string fileType)
        {
            return VideoTypes.Contains(fileType);
        }
        public Image RotateImage(Image originalImage, out bool changed)
        {
            changed = false;
            if (originalImage.PropertyIdList.Contains(0x0112))
            {
                int rotationValue = originalImage.GetPropertyItem(0x0112).Value[0];
                switch (rotationValue)
                {
                    case 1: // landscape, do nothing
                        break;

                    case 8: // rotated 90 right
                            // de-rotate:
                        changed = true;
                        originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate270FlipNone);
                        break;

                    case 3: // bottoms up
                        changed = true;
                        originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate180FlipNone);
                        break;

                    case 6: // rotated 90 left
                        changed = true;
                        originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate90FlipNone);
                        break;
                }
            }
            return originalImage;
        }

        public bool IsImage(string fileType)
        {
            fileType = fileType.Replace(".", "");
            return ImageTypes.Contains(fileType.ToLower().Trim());
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


        public string UploadFileBase64(string fileNamePreWord, string base64, string fileType)
        {
            var folderName = Path.Combine("Resources", "Downloads");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }
            if (base64.StringNotNullOrEmpty())
            {
                var guid = Guid.NewGuid().ToString().Substring(0, 4);
                var extension = fileType.StartsWith(".") ? fileType : "." + fileType;
                var dateTime = $@"{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}_{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}_{DateTime.Now.Millisecond}";
                var fileName = $@"odatv_{fileType.Replace(".", "")}_{fileNamePreWord}_{dateTime}_{guid}{extension}";
                var fullPath = Path.Combine(pathToSave, fileName);

                var bytes = Convert.FromBase64String(base64);
                System.IO.File.WriteAllBytes(fullPath, bytes);
                return folderName.Replace(@"\", "/") + @"/" + fileName; ;
            }
            return null;
        }

        public bool FileExists(string name, string newspaperDate)
        {
            var dt = DateTime.Parse(newspaperDate, new CultureInfo("tr-TR"));
            var todayFolder = dt.Year + "-" + dt.Month + "-" + dt.Day;
            var folderName = Path.Combine("Resources", "Newspapers", todayFolder);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            name = "main_" + name.ToEnglishStandardUrl();
            var str = Path.Combine(pathToSave, name + ".jpg");
            return System.IO.File.Exists(str);
        }
    }
}
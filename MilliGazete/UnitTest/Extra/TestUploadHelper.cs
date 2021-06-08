using Core.Utilities.File;
using Core.Utilities.Helper.Abstract;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Linq;

namespace UnitTest.Extra
{
    public class TestUploadHelper
    {
        public readonly IUploadHelper _uploadHelper;
        public TestUploadHelper()
        {
            _uploadHelper = MockUploadHelper();
        }

        IUploadHelper MockUploadHelper()
        {
            var helper = new Mock<IUploadHelper>();

            //helper.Setup(f => f.RemoveNewsFileList(It.IsAny<List<string>>()));
            helper.Setup(f => f.UploadFile(It.IsAny<string>(), It.IsAny<IFormFile>())).Returns("/");
            helper.Setup(f => f.FileTypeControl(It.IsAny<string>())).Returns(true);
            helper.Setup(f => f.FileSizeControl(It.IsAny<string>(), It.IsAny<long>()))
                .Returns(new Func<string, long, bool>((fileType, fileSize) =>
                {
                    long fileSizeKb = fileSize / 1024;
                    if (UploadHelper.ImageTypes.Contains(fileType) && fileSizeKb <= 4200) return true;
                    if (UploadHelper.VideoTypes.Contains(fileType) && fileSizeKb <= 500000) return true;
                    if (UploadHelper.AudioTypes.Contains(fileType) && fileSizeKb <= 10000) return true;
                    if (UploadHelper.DocumentTypes.Contains(fileType) && fileSizeKb <= 20000) return true;
                    return false;
                }));
            helper.Setup(f => f.IsVideo(It.IsAny<string>())).Returns(true);
            helper.Setup(f => f.DeleteFile(It.IsAny<string>())).Returns(true);
            return helper.Object;
        }
    }
}

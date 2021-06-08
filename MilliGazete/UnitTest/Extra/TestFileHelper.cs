using Core.Utilities.File;
using Microsoft.AspNetCore.Http;
using Moq;
using System.IO;

namespace UnitTest.Extra
{
    public class TestFileHelper
    {
        public IFileHelper fileHelper { get; set; }
        public TestFileHelper()
        {
            fileHelper = MockFileHelper();
        }

        private IFileHelper MockFileHelper()
        {
            var helper = new Mock<IFileHelper>();
            helper.Setup(f => f.OptimizeVideo(It.IsAny<string>())).Returns(true);
            return helper.Object;
        }
        public IFormFile MockFormFileImage()
        {
            var fileMock = new Mock<IFormFile>();
            var content = "Hello World from a Fake File";
            var fileName = "test.jpg";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            return fileMock.Object;
        }

        public IFormFile MockFormFileBigSizeImage()
        {
            var fileMock = new Mock<IFormFile>();
            var content = "Hello World from a Fake File";
            var fileName = "test.jpg";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(5 * 1024 * 1024);
            return fileMock.Object;
        }

        public IFormFile MockFormFileVeryBigSizeImage()
        {
            var fileMock = new Mock<IFormFile>();
            var content = "Hello World from a Fake File";
            var fileName = "test.jpg";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(50 * 1024 * 1024);
            return fileMock.Object;
        }
    }
}

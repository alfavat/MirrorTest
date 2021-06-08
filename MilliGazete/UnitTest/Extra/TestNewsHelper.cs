using Business.Helpers.Abstract;
using Entity.Dtos;
using Moq;
using System;
using System.Collections.Generic;

namespace UnitTest.Extra
{
    public class TestNewsHelper
    {
        public INewsHelper newsHelper { get; set; }
        public TestNewsHelper()
        {
            newsHelper = MockHelper();
        }

        private INewsHelper MockHelper()
        {
            var helper = new Mock<INewsHelper>();
            helper.Setup(f => f.ShortenDescription(It.IsAny<List<NewsViewDto>>()))
                    .Callback(new Action<List<NewsViewDto>>(list => list.ForEach(f => f.ShortDescription = f.ShortDescription.ShortenText(150))))
                    .Returns(new Func<List<NewsViewDto>, List<NewsViewDto>>(list => list));
            return helper.Object;
        }
    }
}

using Business.Helpers.Abstract;
using Entity.Dtos;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest.Extra
{
    public class TestNewsCommentsHelper
    {
        public readonly INewsCommentsHelper _helper;
        public TestNewsCommentsHelper()
        {
            _helper = MockHelper();
        }

        INewsCommentsHelper MockHelper()
        {
            var helper = new Mock<INewsCommentsHelper>();

            helper.Setup(f => f.GetLikeStatus(It.IsAny<List<UserNewsCommentDto>>(), It.IsAny<int>()))
                .Returns(new Func<List<UserNewsCommentDto>, int, List<UserNewsCommentDto>>((data, requestUserId) =>
                {
                    data.ForEach((comment) =>
                    {
                        if (comment.NewsCommentLikeList.HasValue() &&
                        comment.NewsCommentLikeList.Any(f => f.UserId == requestUserId))
                            comment.LikeStatus = comment.NewsCommentLikeList.First(f => f.UserId == requestUserId).IsLike;
                        else
                            comment.LikeStatus = null;
                        comment.NewsCommentLikeList = null;
                    });
                    return data;
                }));

            return helper.Object;
        }
    }
}

using Business.Helpers.Abstract;
using Entity.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace Business.Helpers.Concrete
{
    public class NewsCommentsHelper : INewsCommentsHelper
    {
        public List<UserNewsCommentDto> GetLikeStatus(List<UserNewsCommentDto> data, int requestUserId)
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
        }
    }
}

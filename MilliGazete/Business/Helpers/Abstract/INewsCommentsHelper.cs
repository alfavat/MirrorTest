using Entity.Dtos;
using System.Collections.Generic;

namespace Business.Helpers.Abstract
{
    public interface INewsCommentsHelper
    {
        List<UserNewsCommentDto> GetLikeStatus(List<UserNewsCommentDto> list, int requestUserId);
    }
}

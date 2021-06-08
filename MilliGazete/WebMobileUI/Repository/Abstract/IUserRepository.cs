using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using WebMobileUI.Models;

namespace WebMobileUI.Repository.Abstract
{
    public interface IUserRepository
    {
        IDataResult<UserViewDto> GetProfile(string token);
        IResult UpdateProfile(string name, string surname, string email, string token);
        IResult UpdatePassword(string oldPassword, string newPassword, string token);
        IDataResult<List<NewsBookmarkDto>> GetNewsBookmarks(string token);
        IResult AddNewsBookmark(int id, string token);
        IResult DeleteNewsBookmark(int id, string token);
        IDataResult<NewsBookmarkDto> GetNewsBookmarkByUrl(string url, string token);
        PagingResult<List<NewsCommentDto>> GetNewsComments(string token);
        IResult AddNewsComment(int id, string comment, string token);
        IResult DeleteNewsComment(int id, string token);
    }
}

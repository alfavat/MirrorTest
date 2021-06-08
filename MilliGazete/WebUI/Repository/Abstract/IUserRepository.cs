using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Repository.Abstract
{
    public interface IUserRepository
    {
        Task<IDataResult<UserViewDto>> GetProfile(string token);
        Task<IResult> UpdateProfile(string name, string surname, string email, string token);
        Task<IResult> UpdatePassword(string oldPassword, string newPassword, string token);
        Task<IDataResult<List<NewsBookmarkDto>>> GetNewsBookmarks(string token);
        Task<IResult> AddNewsBookmark(int id, string token);
        Task<IResult> DeleteNewsBookmark(int id, string token);
        Task<IDataResult<NewsBookmarkDto>> GetNewsBookmarkByUrl(string url, string token);
        Task<PagingResult<List<NewsCommentDto>>> GetNewsComments(string token);
        Task<IResult> AddNewsComment(int id, string comment, string token);
        Task<IResult> DeleteNewsComment(int id, string token);
    }
}

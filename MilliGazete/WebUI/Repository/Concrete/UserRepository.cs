using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Models;
using WebUI.Repository.Abstract;

namespace WebUI.Repository.Concrete
{
    public class UserRepository : IUserRepository
    {
        public async Task<IDataResult<UserViewDto>> GetProfile(string token)
        {
            return await ApiHelper.GetApiAsync<UserViewDto>("Users/getcurrentuser", token);
        }

        public async Task<IResult> UpdateProfile(string name, string surname, string email, string token)
        {
            RegisterDto register = new RegisterDto();
            register.FirstName = name;
            register.LastName = surname;
            register.Email = email;
            register.UserName = email;
            return await ApiHelper.PostNoDataApiAsync("Users/updatecurrentuser", register, token);
        }

        public async Task<IResult> UpdatePassword(string oldPassword, string newPassword, string token)
        {
            ChangePassword pass = new ChangePassword();
            pass.Password = oldPassword;
            pass.NewPassword = newPassword;
            pass.ConfirmPassword = newPassword;
            return await ApiHelper.PostNoDataApiAsync("Users/changepassword", pass, token);
        }

        public async Task<IDataResult<List<NewsBookmarkDto>>> GetNewsBookmarks(string token)
        {
            return await ApiHelper.GetApiAsync<List<NewsBookmarkDto>>("NewsBookmarks/getlist", token);
        }

        public async Task<IResult> AddNewsBookmark(int id, string token)
        {
            NewsBookmark newsBookmark = new NewsBookmark();
            newsBookmark.NewsId = id;
            return await ApiHelper.PostNoDataApiAsync("NewsBookmarks/add", newsBookmark, token);
        }

        public async Task<IResult> DeleteNewsBookmark(int id, string token)
        {
            return await ApiHelper.PostNoDataApiAsync("NewsBookmarks/deletebynewsid?newsId=" + id, null, token);
        }

        public async Task<IDataResult<NewsBookmarkDto>> GetNewsBookmarkByUrl(string url, string token)
        {
            return await ApiHelper.GetApiAsync<NewsBookmarkDto>("NewsBookmarks/getlist?url=" + url, token);
        }

        public async Task<PagingResult<List<NewsCommentDto>>> GetNewsComments(string token)
        {
            string param = "?query=&limit=100&orderBy=Id&page=1&ascending=1";
            return await ApiHelper.GetPagingApiAsync<List<NewsCommentDto>>("NewsComments/getusercommentlistbypaging" + param, token);
        }

        public async Task<IResult> AddNewsComment(int id, string comment, string token)
        {
            NewsComment newsComment = new NewsComment();
            newsComment.newsId = id;
            newsComment.title = comment;
            newsComment.content = comment;
            return await ApiHelper.PostNoDataApiAsync("NewsComments/add", newsComment, token);
        }

        public async Task<IResult> DeleteNewsComment(int id, string token)
        {
            NewsComment newsComment = new NewsComment();
            return await ApiHelper.PostNoDataApiAsync("NewsComments/deleteusercommentbyid?newsCommentId=" + id, newsComment, token);
        }
    }
}

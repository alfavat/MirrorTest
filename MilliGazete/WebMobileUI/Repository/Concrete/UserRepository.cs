using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using WebMobileUI.Models;
using WebMobileUI.Repository.Abstract;

namespace WebMobileUI.Repository.Concrete
{
    public class UserRepository : IUserRepository
    {
        public IDataResult<UserViewDto> GetProfile(string token)
        {
            return ApiHelper.GetApi<UserViewDto>("Users/getcurrentuser", token);
        }

        public IResult UpdateProfile(string name, string surname, string email, string token)
        {
            RegisterDto register = new RegisterDto();
            register.FirstName = name;
            register.LastName = surname;
            register.Email = email;
            register.UserName = email;
            return ApiHelper.PostNoDataApi("Users/updatecurrentuser", register, token);
        }

        public IResult UpdatePassword(string oldPassword, string newPassword, string token)
        {
            ChangePassword pass = new ChangePassword();
            pass.Password = oldPassword;
            pass.NewPassword = newPassword;
            pass.ConfirmPassword = newPassword;
            return ApiHelper.PostNoDataApi("Users/changepassword", pass, token);
        }

        public IDataResult<List<NewsBookmarkDto>> GetNewsBookmarks(string token)
        {
            return ApiHelper.GetApi<List<NewsBookmarkDto>>("NewsBookmarks/getlist", token);
        }

        public IResult AddNewsBookmark(int id, string token)
        {
            NewsBookmark newsBookmark = new NewsBookmark();
            newsBookmark.NewsId = id;
            return ApiHelper.PostNoDataApi("NewsBookmarks/add", newsBookmark, token);
        }

        public IResult DeleteNewsBookmark(int id, string token)
        {
            NewsBookmark newsBookmark = new NewsBookmark();
            return ApiHelper.PostNoDataApi("NewsBookmarks/deletebynewsid?newsId=" + id, newsBookmark, token);
        }

        public IDataResult<NewsBookmarkDto> GetNewsBookmarkByUrl(string url, string token)
        {
            return ApiHelper.GetApi<NewsBookmarkDto>("NewsBookmarks/getlist?url=" + url, token);
        }

        public PagingResult<List<NewsCommentDto>> GetNewsComments(string token)
        {
            string param = "?query=&limit=100&orderBy=Id&page=1&ascending=1";
            return ApiHelper.GetPagingApi<List<NewsCommentDto>>("NewsComments/getusercommentlistbypaging" + param, token);
        }

        public IResult AddNewsComment(int id, string comment, string token)
        {
            NewsComment newsComment = new NewsComment();
            newsComment.newsId = id;
            newsComment.title = comment;
            newsComment.content = comment;
            return ApiHelper.PostNoDataApi("NewsComments/add", newsComment, token);
        }

        public IResult DeleteNewsComment(int id, string token)
        {
            NewsComment newsComment = new NewsComment();
            return ApiHelper.PostNoDataApi("NewsComments/deleteusercommentbyid?newsCommentId=" + id, newsComment, token);
        }
    }
}

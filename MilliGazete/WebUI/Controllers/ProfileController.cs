using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;
using WebUI.Repository.Abstract;

namespace WebUI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHelper _tokenHelper;

        public ProfileController(IUserRepository userRepository, ITokenHelper tokenHelper)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
        }

        [Route("profilim")]
        public async Task<IActionResult> Index()
        {
            string token = Request.Cookies["Token"];
            if (token.StringIsNullOrEmpty())
            {
                return Redirect("~/login");
            }
            if (!_tokenHelper.ValidateToken(token))
            {
                Response.Cookies.Delete("Token");
                return Redirect("~/login");
            }
            var result = await _userRepository.GetProfile(token);
            if (result.DataResultIsNotNull())
            {
                return View(result.Data);
            }
            Response.Cookies.Delete("Token");
            return Redirect("~/login");
        }

        [HttpPost]
        [Route("form-profile")]
        public async Task<IResult> FormProfileAsync([FromBody] RegisterUser register)
        {
            string token = Request.Cookies["Token"];
            if (token.StringIsNullOrEmpty() || !_tokenHelper.ValidateToken(token))
            {
                Response.Cookies.Delete("Token");
                return new ErrorResult(Translator.GetByKey("messageAuthenticationDenied"));
            }
            return await _userRepository.UpdateProfile(register.name, register.surname, register.mail, token);
        }

        [Route("sifremi-guncelle")]
        public IActionResult UpdatePassword()
        {
            string token = Request.Cookies["Token"];
            if (token.StringNotNullOrEmpty())
            {
                if (_tokenHelper.ValidateToken(token))
                {
                    return View();
                }
            }
            Response.Cookies.Delete("Token");
            return Redirect("~/login");
        }

        [HttpPost]
        [Route("form-password")]
        public async Task<IResult> FormPasswordAsync([FromBody] ChangePassword pass)
        {
            string token = Request.Cookies["Token"];
            if (token.StringIsNullOrEmpty() || !_tokenHelper.ValidateToken(token))
            {
                Response.Cookies.Delete("Token");
                return new ErrorResult(Translator.GetByKey("messageAuthenticationDenied"));
            }
            return await _userRepository.UpdatePassword(pass.Password, pass.NewPassword, token);
        }

        [Route("kayitli-haberlerim")]
        public async Task<IActionResult> NewsBookmarks()
        {
            string token = Request.Cookies["Token"];
            if (token.StringNotNullOrEmpty())
            {
                if (_tokenHelper.ValidateToken(token))
                {
                    var result = await _userRepository.GetNewsBookmarks(token);
                    if (result.Success)
                    {
                        var groupData = result.Data.
                        GroupBy(x => new
                        {
                            x.FullName,
                            x.ImageUrl,
                            x.NewsId,
                            x.ShortDescription,
                            x.Title,
                            x.Url,
                            x.UserId
                        }).Select(s => new NewsBookmarkDto()
                        {
                            FullName = s.Key.FullName,
                            ImageUrl = s.Key.ImageUrl,
                            NewsId = s.Key.NewsId,
                            ShortDescription = s.Key.ShortDescription,
                            Title = s.Key.Title,
                            Url = s.Key.Url,
                            UserId = s.Key.UserId
                        }).ToList();
                        return View(groupData);
                    }
                }
            }
            return Redirect("~/login");
        }

        [HttpPost]
        [Route("delete-bookmark")]
        public async Task<IResult> DeleteBookmarkAsync([FromBody] NewsBookmark bookmark)
        {
            string token = Request.Cookies["Token"];
            if (token.StringIsNullOrEmpty() || !_tokenHelper.ValidateToken(token))
            {
                Response.Cookies.Delete("Token");
                return new ErrorResult(Translator.GetByKey("messageAuthenticationDenied"));
            }
            return await _userRepository.DeleteNewsBookmark(bookmark.NewsId, token);
        }

        [HttpPost]
        [Route("add-bookmark")]
        public async Task<IResult> AddBookmarkAsync([FromBody] NewsBookmark bookmark)
        {
            string token = Request.Cookies["Token"];
            if (token.StringIsNullOrEmpty() || !_tokenHelper.ValidateToken(token))
            {
                Response.Cookies.Delete("Token");
                return new ErrorResult(Translator.GetByKey("messageAuthenticationDenied"));
            }
            return await _userRepository.AddNewsBookmark(bookmark.NewsId, token);
        }

        [Route("yorumlarim")]
        public async Task<IActionResult> NewsComments()
        {
            string token = Request.Cookies["Token"];
            if (token.StringNotNullOrEmpty())
            {
                if (_tokenHelper.ValidateToken(token))
                {
                    var result = await _userRepository.GetNewsComments(token);
                    if (result.DataResultIsNotNull())
                    {
                        return View(result.Data.Data);
                    }
                }
            }
            return Redirect("~/login");
        }

        [HttpPost]
        [Route("add-comment")]
        public async Task<IResult> AddCommentAsync([FromBody] NewsComment comment)
        {
            string token = Request.Cookies["Token"];
            if (token.StringIsNullOrEmpty() || !_tokenHelper.ValidateToken(token))
            {
                Response.Cookies.Delete("Token");
                return new ErrorResult(Translator.GetByKey("messageAuthenticationDenied"));
            }
            return await _userRepository.AddNewsComment(comment.newsId, comment.content, token);
        }

        [HttpPost]
        [Route("delete-comment")]
        public async Task<IResult> DeleteCommentAsync([FromBody] NewsComment comment)
        {
            string token = Request.Cookies["Token"];
            if (token.StringIsNullOrEmpty() || !_tokenHelper.ValidateToken(token))
            {
                Response.Cookies.Delete("Token");
                return new ErrorResult(Translator.GetByKey("messageAuthenticationDenied"));
            }
            return await _userRepository.DeleteNewsComment(comment.newsId, token);
        }

        [Route("cikis")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("Token");
            return Redirect("~/");
        }
    }
}

using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebMobileUI.Models;
using WebMobileUI.Repository.Abstract;

namespace WebMobileUI.Controllers
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
        public IActionResult Index()
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
            var result = _userRepository.GetProfile(token);
            if (result.DataResultIsNotNull())
            {
                return View(result.Data);
            }
            Response.Cookies.Delete("Token");
            return Redirect("~/login");
        }

        [HttpPost]
        [Route("form-profile")]
        public IResult FormProfile([FromBody] RegisterUser register)
        {
            string token = Request.Cookies["Token"];
            if (token.StringIsNullOrEmpty() || !_tokenHelper.ValidateToken(token))
            {
                Response.Cookies.Delete("Token");
                return new ErrorResult(Translator.GetByKey("messageAuthenticationDenied"));
            }
            return _userRepository.UpdateProfile(register.name, register.surname, register.mail, token);
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
        public IResult FormPassword([FromBody] ChangePassword pass)
        {
            string token = Request.Cookies["Token"];
            if (token.StringIsNullOrEmpty() || !_tokenHelper.ValidateToken(token))
            {
                Response.Cookies.Delete("Token");
                return new ErrorResult(Translator.GetByKey("messageAuthenticationDenied"));
            }
            return _userRepository.UpdatePassword(pass.Password, pass.NewPassword, token);
        }

        [Route("kayitli-haberlerim")]
        public IActionResult NewsBookmarks()
        {
            string token = Request.Cookies["Token"];
            if (token.StringNotNullOrEmpty())
            {
                if (_tokenHelper.ValidateToken(token))
                {
                    var result = _userRepository.GetNewsBookmarks(token);
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
        public IResult DeleteBookmark([FromBody] NewsBookmark bookmark)
        {
            string token = Request.Cookies["Token"];
            if (token.StringIsNullOrEmpty() || !_tokenHelper.ValidateToken(token))
            {
                Response.Cookies.Delete("Token");
                return new ErrorResult(Translator.GetByKey("messageAuthenticationDenied"));
            }
            return _userRepository.DeleteNewsBookmark(bookmark.NewsId, token);
        }

        [HttpPost]
        [Route("add-bookmark")]
        public IResult AddBookmark([FromBody] NewsBookmark bookmark)
        {
            string token = Request.Cookies["Token"];
            if (token.StringIsNullOrEmpty() || !_tokenHelper.ValidateToken(token))
            {
                Response.Cookies.Delete("Token");
                return new ErrorResult(Translator.GetByKey("messageAuthenticationDenied"));
            }
            return _userRepository.AddNewsBookmark(bookmark.NewsId, token);
        }

        [Route("yorumlarim")]
        public IActionResult NewsComments()
        {
            string token = Request.Cookies["Token"];
            if (token.StringNotNullOrEmpty())
            {
                if (_tokenHelper.ValidateToken(token))
                {
                    var result = _userRepository.GetNewsComments(token);
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
        public IResult AddComment([FromBody] NewsComment comment)
        {
            string token = Request.Cookies["Token"];
            if (token.StringIsNullOrEmpty() || !_tokenHelper.ValidateToken(token))
            {
                Response.Cookies.Delete("Token");
                return new ErrorResult(Translator.GetByKey("messageAuthenticationDenied"));
            }
            return _userRepository.AddNewsComment(comment.newsId, comment.content, token);
        }

        [HttpPost]
        [Route("delete-comment")]
        public IResult DeleteComment([FromBody] NewsComment comment)
        {
            string token = Request.Cookies["Token"];
            if (token.StringIsNullOrEmpty() || !_tokenHelper.ValidateToken(token))
            {
                Response.Cookies.Delete("Token");
                return new ErrorResult(Translator.GetByKey("messageAuthenticationDenied"));
            }
            return _userRepository.DeleteNewsComment(comment.newsId, token);
        }

        [Route("cikis")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("Token");
            return Redirect("~/");
        }
    }
}

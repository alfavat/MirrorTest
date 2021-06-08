using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Models;
using WebUI.Repository.Abstract;

namespace WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginRepository _loginRepository;
        private readonly ITokenHelper _tokenHelper;

        public LoginController(ILoginRepository loginRepository, ITokenHelper tokenHelper)
        {
            _loginRepository = loginRepository;
            _tokenHelper = tokenHelper;
        }

        [Route("login")]
        [Route("giris")]
        public IActionResult Index()
        {
            string token = Request.Cookies["Token"];
            if (token.StringNotNullOrEmpty())
            {
                if (_tokenHelper.ValidateToken(token))
                {
                    return Redirect("~/profilim");
                }
                Response.Cookies.Delete("Token");
            }
            return View();
        }

        [HttpPost]
        [Route("form-login")]
        public async Task<IDataResult<AccessToken>> FormLoginAsync([FromBody] RegisterUser login)
        {
            return await _loginRepository.Login(login.mail, login.password);
        }

        [HttpPost]
        [Route("form-register")]
        public async Task<IResult> FormRegisterAsync([FromBody] RegisterUser register)
        {
            return await _loginRepository.Register(register.name, register.surname, register.mail, register.password);
        }

        [HttpPost]
        [Route("form-reset-password")]
        public async Task<IResult> FormResetPasswordAsync([FromBody] RegisterUser user)
        {
            return await _loginRepository.SendPasswordLink(user.mail);
        }

        [Route("sifre-olustur")]
        public IActionResult CreatePassword(int id = 0, string token = "")
        {
            if (id <= 0 || token.StringIsNullOrEmpty())
            {
                return Redirect("/index");
            }

            var resetPassword = new ResetPasswordDto()
            {
                Id = id,
                Token = token
            };
            return View(resetPassword);
        }

        [HttpPost("form-create-password")]
        public async Task<IResult> FormCreatePasswordAsync([FromBody] ResetPasswordDto resetPassword)
        {
            return await _loginRepository.ResetPassword(resetPassword.Id, resetPassword.Token, resetPassword.NewPassword);
        }
    }
}

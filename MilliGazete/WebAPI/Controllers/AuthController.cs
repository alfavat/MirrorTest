using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : MainController
    {
        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = await _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin);
            }
            var res = _authService.CreateAccessToken(userToLogin.Data);
            return GetResponse(res);
        }

        [HttpGet("refreshtoken")]
        public async Task<IActionResult> RefreshToken()
        {
            var userToLogin = await _authService.GetAndCheckCurrentUser();
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin);
            }
            return GetResponse(_authService.CreateAccessToken(userToLogin.Data));
        }

        [HttpGet("createandsendpasswordlink")]
        public async Task<IActionResult> CreateAndSendResetPasswordLink(string userNameOrEmail, string callBackUrl = null)
        {
            return GetResponse(await _authService.CreateAndSendResetPasswordLink(userNameOrEmail, callBackUrl));
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            return GetResponse(await _authService.ResetPassword(resetPasswordDto));
        }
    }
}
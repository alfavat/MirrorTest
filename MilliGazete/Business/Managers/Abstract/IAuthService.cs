using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entity.Dtos;
using Entity.Models;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IAuthService
    {
        Task<IDataResult<User>> Login(UserForLoginDto userForLoginDto);
        Task<IDataResult<User>> GetAndCheckCurrentUser();
        IDataResult<AccessToken> CreateAccessToken(User user);
        Task<IResult> UserExists(string emailOrUserName);
        Task<IResult> CreateAndSendResetPasswordLink(string userNameOrEmail, string callBackUrl);
        Task<IResult> ResetPassword(ResetPasswordDto resetPasswordDto);
    }
}

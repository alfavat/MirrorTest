using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entity.Dtos;
using WebMobileUI.Models;
using WebMobileUI.Repository.Abstract;

namespace WebMobileUI.Repository.Concrete
{
    public class LoginRepository : ILoginRepository
    {
        public IDataResult<AccessToken> Login(string email, string password)
        {
            UserForLoginDto userForLogin = new UserForLoginDto();
            userForLogin.EmailOrUserName = email;
            userForLogin.Password = password;
            return ApiHelper.PostDataApi<AccessToken>("Auth/login", userForLogin);
        }

        public IResult Register(string name, string surname, string email, string password)
        {
            RegisterDto register = new RegisterDto();
            register.FirstName = name;
            register.LastName = surname;
            register.Email = email;
            register.UserName = email;
            register.Password = password;
            register.PasswordConfirm = password;
            return ApiHelper.PostNoDataApi("Users/register", register);
        }

        public IResult SendPasswordLink(string email)
        {
            string param = "?userNameOrEmail=" + email + "&callBackUrl=" + LayoutModel.WebUIUrl + "/sifre-olustur";
            var res = ApiHelper.GetApi<IResult>("Auth/createandsendpasswordlink" + param);
            return res;
        }

        public IResult ResetPassword(int id, string token, string password)
        {
            ResetPasswordDto resetPassword = new ResetPasswordDto();
            resetPassword.Id = id;
            resetPassword.Token = token;
            resetPassword.NewPassword = password;
            resetPassword.ConfirmPassword = password;
            var res = ApiHelper.PostNoDataApi("Auth/resetpassword", resetPassword);
            return res;
        }
    }
}

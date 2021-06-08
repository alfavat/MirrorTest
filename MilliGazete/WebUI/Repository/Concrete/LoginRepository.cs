using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entity.Dtos;
using System.Threading.Tasks;
using WebUI.Models;
using WebUI.Repository.Abstract;

namespace WebUI.Repository.Concrete
{
    public class LoginRepository : ILoginRepository
    {
        public async Task<IDataResult<AccessToken>> Login(string email, string password)
        {
            UserForLoginDto userForLogin = new UserForLoginDto();
            userForLogin.EmailOrUserName = email;
            userForLogin.Password = password;
            return await ApiHelper.PostDataApiAsync<AccessToken>("Auth/login", userForLogin);
        }

        public async Task<IResult> Register(string name, string surname, string email, string password)
        {
            RegisterDto register = new RegisterDto();
            register.FirstName = name;
            register.LastName = surname;
            register.Email = email;
            register.UserName = email;
            register.Password = password;
            register.PasswordConfirm = password;
            return await ApiHelper.PostNoDataApiAsync("Users/register", register);
        }

        public async Task<IResult> SendPasswordLink(string email)
        {
            string param = "?userNameOrEmail=" + email + "&callBackUrl=" + LayoutModel.WebUIUrl + "/sifre-olustur";
            return await ApiHelper.GetApiAsync<IResult>("Auth/createandsendpasswordlink" + param);
        }

        public async Task<IResult> ResetPassword(int id, string token, string password)
        {
            ResetPasswordDto resetPassword = new ResetPasswordDto();
            resetPassword.Id = id;
            resetPassword.Token = token;
            resetPassword.NewPassword = password;
            resetPassword.ConfirmPassword = password;
            return await ApiHelper.PostNoDataApiAsync("Auth/resetpassword", resetPassword);
        }

    }
}

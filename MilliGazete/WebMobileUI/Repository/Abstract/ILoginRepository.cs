using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;

namespace WebMobileUI.Repository.Abstract
{
    public interface ILoginRepository : IUIBaseRepository
    {
        IDataResult<AccessToken> Login(string email, string password);
        IResult Register(string name, string surname, string email, string password);
        IResult SendPasswordLink(string email);
        IResult ResetPassword(int id, string token, string password);
    }
}

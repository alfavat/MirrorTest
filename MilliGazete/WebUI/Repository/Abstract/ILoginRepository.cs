using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using System.Threading.Tasks;

namespace WebUI.Repository.Abstract
{
    public interface ILoginRepository : IUIBaseRepository
    {
        Task<IDataResult<AccessToken>> Login(string email, string password);
        Task<IResult> Register(string name, string surname, string email, string password);
        Task<IResult> SendPasswordLink(string email);
        Task<IResult> ResetPassword(int id, string token, string password);
    }
}

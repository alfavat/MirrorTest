using Core.Utilities.Results;
using Entity.Models;
using System.Threading.Tasks;

namespace WebUI.Repository.Abstract
{
    public interface IOptionRepository
    {
        Task<IDataResult<Option>> GetOption();
    }
}

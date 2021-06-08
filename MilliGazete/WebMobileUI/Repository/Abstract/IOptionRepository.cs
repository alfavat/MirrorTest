using Core.Utilities.Results;
using Entity.Models;

namespace WebMobileUI.Repository.Abstract
{
    public interface IOptionRepository
    {
        IDataResult<Option> GetOption();
    }
}

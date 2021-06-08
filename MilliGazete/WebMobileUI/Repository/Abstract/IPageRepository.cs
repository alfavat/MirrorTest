using Core.Utilities.Results;
using Entity.Dtos;

namespace WebMobileUI.Repository.Abstract
{
    public interface IPageRepository : IUIBaseRepository
    {
        IDataResult<PageDto> GetByUrl(string url = "");
    }
}

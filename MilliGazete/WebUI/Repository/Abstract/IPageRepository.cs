using Core.Utilities.Results;
using Entity.Dtos;
using System.Threading.Tasks;

namespace WebUI.Repository.Abstract
{
    public interface IPageRepository : IUIBaseRepository
    {
        Task<IDataResult<PageDto>> GetByUrl(string url = "");
    }
}

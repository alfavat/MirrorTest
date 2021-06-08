using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Repository.Abstract
{
    public interface ICategoryPageRepository : IUIBaseRepository
    {
        Task<IDataResult<List<MainHeadingDto>>> GetLastMainHeadingNewsByCategoryUrl(string categoryUrl, int limit = 0);
        Task<IDataResult<List<MainPageCategoryNewsDto>>> GetLastNewsByCategoryUrl(string categoryUrl, int limit = 0);
        Task<IDataResult<CategoryDto>> GetByUrl(string categoryUrl);
    }
}

using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface ICategoryPageService
    {
        Task<IDataResult<CategoryDto>> GetByUrl(string url);
        Task<IDataResult<List<MainPageCategoryNewsDto>>> GetLastNewsByCategoryUrl(string url, int limit);
        Task<IDataResult<List<MainHeadingDto>>> GetLastMainHeadingNewsByCategoryUrl(string url, int limit);
        Task<IDataResult<List<MainPageCategoryNewsDto>>> GetLastNewsByCategoryId(int id, int limit);
        Task<IDataResult<List<MainHeadingDto>>> GetLastMainHeadingNewsByCategoryId(int id, int limit);
        Task<IDataResult<CategoryDto>> GetById(int id);
    }
}

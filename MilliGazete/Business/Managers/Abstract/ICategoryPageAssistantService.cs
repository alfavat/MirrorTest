using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface ICategoryPageAssistantService
    {
        Task<List<MainPageCategoryNewsDto>> GetLastNewsByCategoryUrl(string url , int limit);
        Task<List<MainHeadingDto>> GetTopMainHeadingNewsByCategoryUrl(string url, int limit);
        Task<List<MainPageCategoryNewsDto>> GetLastNewsByCategoryId(int id, int limit);
        Task<List<MainHeadingDto>> GetTopMainHeadingNewsByCategoryId(int id, int limit);
    }
}

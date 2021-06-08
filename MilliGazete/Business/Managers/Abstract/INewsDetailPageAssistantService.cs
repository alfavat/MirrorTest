using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface INewsDetailPageAssistantService
    {
        List<NewsDetailPageDto> GetNewsWithDetailsByPaging(MainPageNewsPagingDto pagingDto, out int total, int? requestedUserId = null);
        Task<NewsDetailPageDto> GetNewsWithDetails(string url, int? id = null, bool preview = false, int? requestedUserId = null);
        Task<List<MostViewedNewsDto>> GetLastWeekMostViewedNews(int limit);
        Task<List<MostSharedNewsDto>> GetMostShareNewsList(int limit);
    }
}

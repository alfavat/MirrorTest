using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface INewsDetailPageAssistantService
    {
        Task<Tuple<List<NewsDetailPageDto>, int>> GetNewsWithDetailsByPaging(MainPageNewsPagingDto pagingDto, int? requestedUserId = null);
        Task<NewsDetailPageDto> GetNewsWithDetails(string url, int? id = null, bool preview = false, int? requestedUserId = null);
        Task<List<MostViewedNewsDto>> GetLastWeekMostViewedNews(int limit);
        Task<List<MostSharedNewsDto>> GetMostShareNewsList(int limit);
    }
}

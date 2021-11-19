using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface INewsDetailPageAssistantService
    {
        Task<Tuple<List<NewsDetailPageDto>, int>> GetNewsWithDetailsByPaging(MainPageNewsPagingDto pagingDto);
        Task<Tuple<List<NewsDetailPagePagingDto>, int>> GetNewsWithDetailsByPaging2(MainPageNewsPagingDto pagingDto);
        Task<NewsDetailPageDto> GetNewsWithDetails(string url, int? id = null, bool preview = false);
        Task<List<MostViewedNewsDto>> GetLastWeekMostViewedNews(int limit);
        Task<List<MostSharedNewsDto>> GetMostShareNewsList(int limit);
    }
}

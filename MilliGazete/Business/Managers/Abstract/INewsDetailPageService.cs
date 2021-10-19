using Core.Utilities.Results;
using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface INewsDetailPageService
    {
        Task<IDataResult<Tuple<List<NewsDetailPageDto>, int>>> GetNewsWithDetailsByPaging(MainPageNewsPagingDto pagingDto);
        Task<IDataResult<NewsDetailPageDto>> GetNewsWithDetails(string url);
        Task<IDataResult<List<MostViewedNewsDto>>> GetLastWeekMostViewedNews(int limit);
        Task<IDataResult<NewsDetailPageDto>> GetNewsWithDetailsById(int id, bool preview = false);
        Task<IDataResult<List<MostSharedNewsDto>>> GetMostShareNewsList(int limit);
    }
}

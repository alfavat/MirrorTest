using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IMainPageAssistantService
    {
        Task<Tuple<List<NewsDetailPageDto>, int>> GetNewsWithDetailsByPaging(MainPageNewsPagingDto pagingDto);
        Task<NewsDetailPageDto> GetNewsWithDetails(string url);
        Task<List<BreakingNewsDto>> GetTopBreakingNews(int limit);
        Task<List<SubHeadingDto>> GetTopSubHeadingNews(int limit);
        Task<List<SubHeadingDto>> GetTopSubHeadingNews2(int limit);
        Task<List<MainHeadingDto>> GetTopMainHeadingNews(int limit);
        Task<List<MainPageNewsDto>> GetTopMainPageNews(int limit);
        Task<List<MainPageVideoNewsDto>> GetLastVideoNews(int limit);
        Task<List<MainPageImageNewsDto>> GetLastImageNews(int limit);
        Task<List<MainPageStoryNewsDto>> GetStoryNews(int limit);
        Task<List<MainPageFourHillNewsDto>> GetTopMainPageFourHillNews(int limit);
        Task<List<WideHeadingNewsDto>> GetTopWideHeadingNews(int limit);
        Task<List<FlashNewsDto>> GetLastFlashNews(int limit);
    }
}

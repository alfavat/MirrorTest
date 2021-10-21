using Entity.Dtos;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface INewsAssistantService
    {
        Task<News> GetById(int newsId);
        Task<NewsViewDto> GetViewById(int newsId);
        Task Update(News news);
        Task<int> Add(NewsAddDto news, int addUserId, int historyNo);
        Task<Tuple<List<NewsPagingViewDto>, int>> GetListByPaging(NewsPagingDto pagingDto);
        Task<List<NewsHistoryDto>> GetListByHistoryNo(int historyNo);
        Task<int> GetMaxHistoryNo();
        Task<List<NewsSiteMapDto>> GetListForSiteMap();
        Task IncreaseShareCount(int newsId);
        Task<NewsViewDto> GetViewByUrl(string url);
        Task<List<ArticleDto>> GetLastWeekMostViewedArticles(int limit);
        Task<List<NewsViewDto>> GetListByAuthorId(int authorId);
        Task<List<NewsViewDto>> GetListByReporterId(int reporterId);
        Task<ArticleDto> GetArticleByUrl(string url);
    }
}

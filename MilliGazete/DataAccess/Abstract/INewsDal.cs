using DataAccess.Base;
using Entity.Dtos;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface INewsDal : IEntityRepository<News>
    {
        Task<Tuple<List<NewsDetailPageDto>, int>> GetNewsWithDetailsByPaging(MainPageNewsPagingDto pagingDto, int? requestedUserId = null);
        Task<List<MostViewedNewsDto>> GetLastWeekMostViewedNews(int limit);
        Task<List<MostSharedNewsDto>> GetMostShareNewsList(int limit);
        Task<NewsDetailPageDto> GetNewsWithDetails(string url, int? id = null, bool preview = false, int? requestedUserId = null);
        Task AddNewsWithDetails(News news, List<NewsCategory> categories, List<NewsFile> files,
          List<NewsPosition> positions, List<NewsProperty> properties,
          List<NewsRelatedNews> newsRelatedNews, List<NewsTag> tags, bool isAdd);
        Task<List<DashboardChartDataDto>> GetDashboardChartData(DateTime fromDate);
        Task<News> GetView(Expression<Func<News, bool>> filter = null);
        Task<DashboardStatisticsDto> GetDashboardStatistics();
        IQueryable<News> GetActiveList();
        IQueryable<News> GetNewsListByCategoryUrl(string url, out int headingPositionEntityId);
        Task<List<DashboardTopCommentNewsDto>> GetDashboardTopCommentNews(int limit);
        IQueryable<News> GetNewsListByTagUrl(string url);
        IQueryable<News> GetNewsListByCategoryId(int id, out int headingPositionEntityId);
        Task IncreaseShareCount(int newsId);
    }
}

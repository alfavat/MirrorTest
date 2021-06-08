using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Repository.Abstract
{
    public interface INewsDetailPageRepository : IUIBaseRepository
    {
        Task<IDataResult<NewsDetail>> GetNewsWithDetails(string url, string token = "");

        Task<IDataResult<NewsDetail>> GetNewsWithDetailsById(string id, bool preview = false, string token = "");

        Task<IDataResult<List<MostViewedNewsDto>>> GetLastWeekMostViewedNews(int limit = 0);

        Task<PagingResult<List<NewsDetail>>> GetNewsWithDetailsByPaging(string url, string query, int limit = 1, string orderBy = "id", int page = 1, int ascending = 1, string token = "");

        Task<PagingResult<List<UserNewsCommentDto>>> GetNewsComments(int id, int limit = 5, int page = 1,string token="");

        Task<IResult> AddOrDeleteCommentLike(int newsCommentId, string token);

        Task<IDataResult<List<MostSharedNewsDto>>> GetMostSharedNewsList(int limit = 0);
    }
}

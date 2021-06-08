using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using WebMobileUI.Models;

namespace WebMobileUI.Repository.Abstract
{
    public interface INewsDetailPageRepository : IUIBaseRepository
    {
        IDataResult<NewsDetail> GetNewsWithDetails(string url, string token = "");

        IDataResult<NewsDetail> GetNewsWithDetailsById(string id, bool preview = false, string token = "");

        IDataResult<List<MostViewedNewsDto>> GetLastWeekMostViewedNews(int limit = 0);

        PagingResult<List<NewsDetail>> GetNewsWithDetailsByPaging(string url, string query, int limit = 1, string orderBy = "id", int page = 1, int ascending = 1, string token = "");

        PagingResult<List<UserNewsCommentDto>> GetNewsComments(int id, int limit = 5, int page = 1, string token = "");
        IResult AddOrDeleteCommentLike(int newsCommentId, string token);
    }
}

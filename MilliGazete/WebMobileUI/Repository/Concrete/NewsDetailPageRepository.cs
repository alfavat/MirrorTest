using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using WebMobileUI.Models;
using WebMobileUI.Repository.Abstract;

namespace WebMobileUI.Repository.Concrete
{
    public class NewsDetailPageRepository : INewsDetailPageRepository
    {
        public IDataResult<NewsDetail> GetNewsWithDetails(string url, string token = "")
        {
            string param = "?url=" + url;
            return ApiHelper.GetApi<NewsDetail>("NewsDetailPage/getnewswithdetails" + param, token);
        }

        public IDataResult<NewsDetail> GetNewsWithDetailsById(string id, bool preview = false, string token = "")
        {
            string param = "?id=" + id + "&preview=" + preview;
            return ApiHelper.GetApi<NewsDetail>("NewsDetailPage/getnewswithdetailsbyid" + param, token);
        }

        public IDataResult<List<MostViewedNewsDto>> GetLastWeekMostViewedNews(int limit = 0)
        {
            string param = "";
            if (limit != 0) param = "?limit=" + limit;
            return ApiHelper.GetApi<List<MostViewedNewsDto>>("NewsDetailPage/getlastweekmostviewednews" + param);
        }

        public PagingResult<List<NewsDetail>> GetNewsWithDetailsByPaging(string url, string query, int limit = 1, string orderBy = "id", int page = 1, int ascending = 1, string token = "")
        {
            string param = "?url=" + url + "&query=" + query + "&limit=" + limit + "&orderBy=" + orderBy + "&page=" + page + "&ascending=" + ascending;
            return ApiHelper.GetPagingApi<List<NewsDetail>>("NewsDetailPage/getnewswithdetailsbypaging" + param, token);
        }

        public PagingResult<List<UserNewsCommentDto>> GetNewsComments(int id, int limit = 5, int page = 1, string token = "")
        {
            return ApiHelper.GetPagingApi<List<UserNewsCommentDto>>("NewsComments/getbynewsid?newsId=" + id + "&limit=" + limit + "&page=" + page, token);
        }

        public IResult AddOrDeleteCommentLike(int newsCommentId, string token)
        {
            return ApiHelper.PostNoDataApi("NewsCommentLikes/addordelete?newsCommentId=" + newsCommentId, null, token);
        }
    }
}

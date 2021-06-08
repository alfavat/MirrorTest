using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Models;
using WebUI.Repository.Abstract;

namespace WebUI.Repository.Concrete
{
    public class NewsDetailPageRepository : INewsDetailPageRepository
    {
        public async Task<IDataResult<NewsDetail>> GetNewsWithDetails(string url, string token = "")
        {
            string param = "?url=" + url;
            return await ApiHelper.GetApiAsync<NewsDetail>("NewsDetailPage/getnewswithdetails" + param, token);
        }

        public async Task<IDataResult<NewsDetail>> GetNewsWithDetailsById(string id, bool preview = false, string token = "")
        {
            string param = "?id=" + id + "&preview=" + preview;
            return await ApiHelper.GetApiAsync<NewsDetail>("NewsDetailPage/getnewswithdetailsbyid" + param, token);
        }

        public async Task<IDataResult<List<MostViewedNewsDto>>> GetLastWeekMostViewedNews(int limit = 0)
        {
            string param = "";
            if (limit != 0) param = "?limit=" + limit;
            return await ApiHelper.GetApiAsync<List<MostViewedNewsDto>>("NewsDetailPage/getlastweekmostviewednews" + param);
        }

        public async Task<PagingResult<List<NewsDetail>>> GetNewsWithDetailsByPaging(string url, string query, int limit = 1, string orderBy = "id", int page = 1, int ascending = 1, string token = "")
        {
            string param = "?url=" + url + "&query=" + query + "&limit=" + limit + "&orderBy=" + orderBy + "&page=" + page + "&ascending=" + ascending;
            return await ApiHelper.GetPagingApiAsync<List<NewsDetail>>("NewsDetailPage/getnewswithdetailsbypaging" + param, token);
        }

        public async Task<PagingResult<List<UserNewsCommentDto>>> GetNewsComments(int id, int limit = 5, int page = 1, string token = "")
        {
            return await ApiHelper.GetPagingApiAsync<List<UserNewsCommentDto>>("NewsComments/getbynewsid?newsId=" + id + "&limit=" + limit + "&page=" + page, token);
        }

        public async Task<IResult> AddOrDeleteCommentLike(int newsCommentId, string token)
        {
            return await ApiHelper.PostNoDataApiAsync("NewsCommentLikes/addordelete?newsCommentId=" + newsCommentId, null, token);
        }

        public async Task<IDataResult<List<MostSharedNewsDto>>> GetMostSharedNewsList(int limit = 0)
        {
            string param = "";
            if (limit != 0) param = "?limit=" + limit;
            return await ApiHelper.GetApiAsync<List<MostSharedNewsDto>>("NewsDetailPage/getmostsharenews" + param);
        }
    }
}

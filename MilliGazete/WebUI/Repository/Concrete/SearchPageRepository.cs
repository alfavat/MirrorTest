using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Models;
using WebUI.Repository.Abstract;

namespace WebUI.Repository.Concrete
{
    public class SearchPageRepository : ISearchPageRepository
    {
        public async Task<PagingResult<List<MainPageSearchNewsDto>>> GetListByPaging(string query, int limit = 10, string orderBy = "Id", int page = 1, int ascending = 1)
        {
            string param = "?query=" + query + "&limit=" + limit + "&orderBy=" + orderBy + "&page=" + page + "&ascending=" + ascending;
            return await ApiHelper.GetPagingApiAsync<List<MainPageSearchNewsDto>>("SearchPage/getlistbypaging" + param);
        }

        public async Task<IDataResult<List<MainPageTagNewsDto>>> GetNewsByTagUrl(string categoryUrl, int limit = 0)
        {
            string param = "?url=" + categoryUrl;
            if (limit != 0) param = "?url=" + categoryUrl + "&limit=" + limit;
            return await ApiHelper.GetApiAsync<List<MainPageTagNewsDto>>("SearchPage/getnewsbytagurl" + param);
        }
    }
}

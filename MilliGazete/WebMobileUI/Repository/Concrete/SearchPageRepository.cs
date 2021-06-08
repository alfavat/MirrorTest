using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using WebMobileUI.Models;
using WebMobileUI.Repository.Abstract;

namespace WebMobileUI.Repository.Concrete
{
    public class SearchPageRepository : ISearchPageRepository
    {
        public PagingResult<List<MainPageSearchNewsDto>> GetListByPaging(string query, int limit = 10, string orderBy = "id", int page = 1, int ascending = 1)
        {
            string param = "?query=" + query + "&limit=" + limit + "&orderBy=" + orderBy + "&page=" + page + "&ascending=" + ascending;
            return ApiHelper.GetPagingApi<List<MainPageSearchNewsDto>>("SearchPage/getlistbypaging" + param);
        }

        public IDataResult<List<MainPageTagNewsDto>> GetNewsByTagUrl(string categoryUrl, int limit = 0)
        {
            string param = "?url=" + categoryUrl;
            if (limit != 0) param = "?url=" + categoryUrl + "&limit=" + limit;
            return ApiHelper.GetApi<List<MainPageTagNewsDto>>("SearchPage/getnewsbytagurl" + param);
        }
    }
}

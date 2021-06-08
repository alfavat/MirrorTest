using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Repository.Abstract;

namespace WebUI.Repository.Concrete
{
    public class CategoryPageRepository : ICategoryPageRepository
    {
        public async Task<IDataResult<List<MainHeadingDto>>> GetLastMainHeadingNewsByCategoryUrl(string categoryUrl, int limit = 0)
        {
            string param = "?url=" + categoryUrl;
            if (limit != 0) param = "?url=" + categoryUrl + "&limit=" + limit;
            return await ApiHelper.GetApiAsync<List<MainHeadingDto>>("CategoryPage/getlastmainheadingnewsbycategoryurl" + param);
        }

        public async Task<IDataResult<List<MainPageCategoryNewsDto>>> GetLastNewsByCategoryUrl(string categoryUrl, int limit = 0)
        {
            string param = "?url=" + categoryUrl;
            if (limit != 0) param = "?url=" + categoryUrl + "&limit=" + limit;
            return await ApiHelper.GetApiAsync<List<MainPageCategoryNewsDto>>("CategoryPage/getlastnewsbycategoryurl" + param);
        }

        public async Task<IDataResult<CategoryDto>> GetByUrl(string categoryUrl)
        {
            string param = "?url=" + categoryUrl;
            return await ApiHelper.GetApiAsync<CategoryDto>("CategoryPage/getbyurl" + param);
        }
    }
}

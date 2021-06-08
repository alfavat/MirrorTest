using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using WebMobileUI.Models;
using WebMobileUI.Repository.Abstract;

namespace WebMobileUI.Repository.Concrete
{
    public class CategoryPageRepository : ICategoryPageRepository
    {
        public IDataResult<List<CustomHeading>> GetLastMainHeadingNewsByCategoryUrl(string categoryUrl, int limit = 0)
        {
            string param = "?url=" + categoryUrl;
            if (limit != 0) param = "?url=" + categoryUrl + "&limit=" + limit;
            return ApiHelper.GetApi<List<CustomHeading>>("CategoryPage/getlastmainheadingnewsbycategoryurl" + param);
        }

        public IDataResult<List<MainPageCategoryNewsDto>> GetLastNewsByCategoryUrl(string categoryUrl, int limit = 0)
        {
            string param = "?url=" + categoryUrl;
            if (limit != 0) param = "?url=" + categoryUrl + "&limit=" + limit;
            return ApiHelper.GetApi<List<MainPageCategoryNewsDto>>("CategoryPage/getlastnewsbycategoryurl" + param);
        }

        public IDataResult<CategoryDto> GetByUrl(string categoryUrl)
        {
            string param = "?url=" + categoryUrl;
            return ApiHelper.GetApi<CategoryDto>("CategoryPage/getbyurl" + param);
        }
    }
}

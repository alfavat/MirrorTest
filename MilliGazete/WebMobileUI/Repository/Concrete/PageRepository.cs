using Core.Utilities.Results;
using Entity.Dtos;
using WebMobileUI.Repository.Abstract;

namespace WebMobileUI.Repository.Concrete
{
    public class PageRepository : IPageRepository
    {
        public IDataResult<PageDto> GetByUrl(string url = "")
        {
            string param = "?url=" + url;
            return ApiHelper.GetApi<PageDto>("Pages/getbyurl" + param);
        }
    }
}

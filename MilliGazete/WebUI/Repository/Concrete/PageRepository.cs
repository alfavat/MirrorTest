using Core.Utilities.Results;
using Entity.Dtos;
using System.Threading.Tasks;
using WebUI.Repository.Abstract;

namespace WebUI.Repository.Concrete
{
    public class PageRepository : IPageRepository
    {
        public async Task<IDataResult<PageDto>> GetByUrl(string url = "")
        {
            string param = "?url=" + url;
            return await ApiHelper.GetApiAsync<PageDto>("Pages/getbyurl" + param);
        }
    }
}

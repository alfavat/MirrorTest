using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Repository.Abstract
{
    public interface ISearchPageRepository : IUIBaseRepository
    {
        Task<IDataResult<List<MainPageTagNewsDto>>> GetNewsByTagUrl(string url="", int limit=20);
        Task<PagingResult<List<MainPageSearchNewsDto>>> GetListByPaging(string query, int limit = 10, string orderBy = "Id", int page = 1, int ascending = 1);
    }
}

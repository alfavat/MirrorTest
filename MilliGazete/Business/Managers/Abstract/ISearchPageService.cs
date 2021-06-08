using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface ISearchPageService
    {
        Task<IDataResult<List<MainPageTagNewsDto>>> GetNewsByTagUrl(string url, int limit);
        IDataResult<List<MainPageSearchNewsDto>> GetListByPaging(NewsPagingDto pagingDto, out int total);
    }
}

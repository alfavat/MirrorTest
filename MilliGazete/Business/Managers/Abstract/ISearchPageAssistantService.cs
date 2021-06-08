using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface ISearchPageAssistantService
    {
        Task<List<MainPageTagNewsDto>> GetNewsByTagUrl(string url , int limit);
        List<MainPageSearchNewsDto> GetNewsListByPaging(NewsPagingDto pagingDto, out int total);
    }
}

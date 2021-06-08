using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using WebMobileUI.Models;

namespace WebMobileUI.Repository.Abstract
{
    public interface ISearchPageRepository : IUIBaseRepository
    {
        PagingResult<List<MainPageSearchNewsDto>> GetListByPaging(string query, int limit = 10, string orderBy = "newsid", int page = 1, int ascending = 1);
        IDataResult<List<MainPageTagNewsDto>> GetNewsByTagUrl(string url="", int limit=20);
    }
}

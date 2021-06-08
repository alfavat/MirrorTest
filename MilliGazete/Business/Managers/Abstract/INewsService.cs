using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface INewsService
    {
        Task<IDataResult<NewsViewDto>> GetViewById(int newsId);
        Task<IDataResult<NewsViewDto>> GetViewByUrl(string url);
        Task<IDataResult<int>> Add(NewsAddDto newsAddDto);
        Task<IResult> Delete(int newsId);
        Task<IResult> ChangeActiveStatus(ChangeActiveStatusDto changeActiveStatusDto);
        Task<IDataResult<List<NewsViewDto>>> GetList();
        IDataResult<List<NewsViewDto>> GetListByPaging(NewsPagingDto pagingDto, out int total);
        Task<IDataResult<List<NewsHistoryDto>>> GetHistoryByNewsId(int newsId);
        Task<IResult> ChangeIsDraftStatus(ChangeIsDraftStatusDto dto);
        Task<IDataResult<List<NewsSiteMapDto>>> GetListForSiteMap();
        Task<IResult> IncreaseShareCount(int newsId);
        Task<IDataResult<List<ArticleDto>>> GetLastWeekMostViewedArticles(int count);
        Task<IDataResult<List<NewsViewDto>>> GetListByAuthorId(int authorId);
    }
}

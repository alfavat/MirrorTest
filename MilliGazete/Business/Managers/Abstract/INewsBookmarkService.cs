using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface INewsBookmarkService
    {
        Task<IResult> Add(NewsBookmarkAddDto dto);
        Task<IResult> DeleteByNewsId(int newsId);
        Task<IDataResult<List<NewsBookmarkDto>>> GetList();
        Task<IDataResult<NewsBookmarkDto>> GetByNewsUrl(string url);
        Task<IDataResult<NewsBookmarkDto>> GetByNewsId(int newsId);
        Task<IDataResult<bool>> HasUserBookmarkedNews(int newsId);
    }
}

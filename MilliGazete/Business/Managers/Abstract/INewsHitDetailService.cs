using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface INewsHitDetailService
    {
        Task<IResult> Add(NewsHitDetailAddDto dto);
        Task<IDataResult<List<NewsHitDetailDto>>> GetList();
        Task<IDataResult<List<NewsHitDetailDto>>> GetListByNewsId(int newsId);
        Task<IDataResult<List<NewsHitDetailDto>>> GetLastNewHitDetails(int minutes);
    }
}

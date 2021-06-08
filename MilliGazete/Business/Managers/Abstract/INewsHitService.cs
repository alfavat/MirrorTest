using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface INewsHitService
    {
        Task<IResult> AddWithDetail(NewsHitAddDto dto);
        Task<IDataResult<List<NewsHitDto>>> GetList();
        Task<IDataResult<List<NewsHitDto>>> GetListByNewsId(int newsId);
    }
}

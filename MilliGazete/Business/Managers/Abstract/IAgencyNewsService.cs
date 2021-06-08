using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IAgencyNewsService
    {
        Task<IDataResult<AgencyNewsViewDto>> GetById(int agencyNewsId);
        Task<IResult> AddArray(List<NewsAgencyAddDto> data);
        Task<IDataResult<List<AgencyNewsViewDto>>> GetList();
        IDataResult<List<AgencyNewsViewDto>> GetListByPaging(NewsAgencyPagingDto pagingDto, out int total);
        Task<IDataResult<int>> CopyNewsFromAgencyNews(AgencyNewsCopyDto dto);
    }
}

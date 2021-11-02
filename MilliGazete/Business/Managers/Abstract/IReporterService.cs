using Core.Utilities.Results;
using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IReporterService
    {
        Task<IDataResult<ReporterDto>> GetById(int reporterId);
        Task<IResult> Update(ReporterUpdateDto reporterUpdateDto);
        Task<IDataResult<int>> Add(ReporterAddDto dto);
        Task<IResult> Delete(int reporterId);
        Task<IDataResult<List<ReporterDto>>> GetList();
        Task<IDataResult<ReporterDto>> GetByUrl(string url);
        Task<IDataResult<Tuple<List<NewsPagingViewDto>, int>>> GetListByPagingViaUrl(ReporterNewsPagingDto pagingDto);
    }
}

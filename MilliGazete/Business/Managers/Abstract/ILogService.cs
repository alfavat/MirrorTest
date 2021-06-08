using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;

namespace Business.Managers.Abstract
{
    public interface ILogService
    {
        IDataResult<List<LogDto>> GetListByPaging(LogPagingDto pagingDto, out int total);
    }
}

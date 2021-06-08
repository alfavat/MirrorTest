using Entity.Dtos;
using System.Collections.Generic;

namespace Business.Managers.Abstract
{
    public interface ILogAssistantService
    {
        List<LogDto> GetListByPaging(LogPagingDto pagingDto, out int total);
    }
}

using Business.BusinessAspects.Autofac;
using Business.Managers.Abstract;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;

namespace Business.Managers.Concrete
{
    public class LogManager : ILogService
    {
        private readonly ILogAssistantService _logAssistantService;
        public LogManager(ILogAssistantService logAssistant)
        {
            _logAssistantService = logAssistant;
        }

        [SecuredOperation("LogGet")]
        [PerformanceAspect()]
        public IDataResult<List<LogDto>> GetListByPaging(LogPagingDto pagingDto, out int total)
        {
            return new SuccessDataResult<List<LogDto>>(_logAssistantService.GetListByPaging(pagingDto, out total));
        }
    }
}

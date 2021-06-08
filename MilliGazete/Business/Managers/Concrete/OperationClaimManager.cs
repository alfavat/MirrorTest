using AutoMapper;
using Business.BusinessAspects.Autofac;
using Business.Managers.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimAssistantService _operationClaimAssistantService;
        private readonly IMapper _mapper;

        public OperationClaimManager(IOperationClaimAssistantService OperationClaimAssistantService, IMapper mapper)
        {
            _operationClaimAssistantService = OperationClaimAssistantService;
            _mapper = mapper;
        }


        [SecuredOperation()]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<OperationClaimDto>>> GetList()
        {
            return new SuccessDataResult<List<OperationClaimDto>>(await _operationClaimAssistantService.GetList());
        }
    }
}

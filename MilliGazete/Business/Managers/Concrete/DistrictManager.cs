using AutoMapper;
using Business.Managers.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class DistrictManager : IDistrictService
    {
        private readonly IDistrictAssistantService _districtAssistantService;
        private readonly IMapper _mapper;

        public DistrictManager(IDistrictAssistantService districtAssistantService, IMapper mapper)
        {
            _districtAssistantService = districtAssistantService;
            _mapper = mapper;
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<DistrictDto>>> GetListByCityId(int id)
        {
            return new SuccessDataResult<List<DistrictDto>>(await _districtAssistantService.GetListByCityId(id));
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<DistrictDto>>> GetList()
        {
            return new SuccessDataResult<List<DistrictDto>>(await _districtAssistantService.GetList());
        }
    }
}

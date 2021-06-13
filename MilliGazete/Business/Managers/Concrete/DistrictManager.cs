using AutoMapper;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Managers.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using Entity.Dtos;
using Entity.Models;
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
    }
}

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
    public class CityManager : ICityService
    {
        private readonly ICityAssistantService _cityAssistantService;
        private readonly IMapper _mapper;

        public CityManager(ICityAssistantService cityAssistantService, IMapper mapper)
        {
            _cityAssistantService = cityAssistantService;
            _mapper = mapper;
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<CityDto>>> GetList()
        {
            return new SuccessDataResult<List<CityDto>>(await _cityAssistantService.GetList());
        }
    }
}

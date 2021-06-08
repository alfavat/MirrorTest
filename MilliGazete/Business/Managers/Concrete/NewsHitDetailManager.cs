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
    public class NewsHitDetailManager : INewsHitDetailService
    {
        private readonly INewsHitDetailAssistantService _newsHitDetailAssistantService;
        private readonly IMapper _mapper;
        private readonly IBaseService _baseService;

        public NewsHitDetailManager(INewsHitDetailAssistantService newsHitDetailAssistantService, IMapper mapper, IBaseService baseService)
        {
            _baseService = baseService;
            _newsHitDetailAssistantService = newsHitDetailAssistantService;
            _mapper = mapper;
        }

        [SecuredOperation("")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<NewsHitDetailDto>>> GetList()
        {
            return new SuccessDataResult<List<NewsHitDetailDto>>(await _newsHitDetailAssistantService.GetList());
        }

        [SecuredOperation("")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<NewsHitDetailDto>>> GetListByNewsId(int newsId)
        {
            return new SuccessDataResult<List<NewsHitDetailDto>>(await _newsHitDetailAssistantService.GetListByNewsId(newsId));
        }

        [PerformanceAspect()]
        public async Task<IDataResult<List<NewsHitDetailDto>>> GetLastNewHitDetails(int minutes)
        {
            return new SuccessDataResult<List<NewsHitDetailDto>>(await _newsHitDetailAssistantService.GetLastNewHitDetails(minutes));
        }

        [ValidationAspect(typeof(NewsHitDetailAddDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("INewsHitDetailService.Get")]
        public async Task<IResult> Add(NewsHitDetailAddDto dto)
        {
            var data = _mapper.Map<NewsHitDetail>(dto);
            data.IpAddress = _baseService.UserIpAddress;
            if (_baseService.RequestUserId > 0)
            {
                data.UserId = _baseService.RequestUserId;
            }
            await _newsHitDetailAssistantService.Add(data);
            return new SuccessResult(Messages.Added);
        }
    }
}

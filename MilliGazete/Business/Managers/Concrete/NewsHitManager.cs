using AutoMapper;
using Business.Constants;
using Business.Managers.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class NewsHitManager : INewsHitService
    {
        private readonly INewsHitAssistantService _newsHitAssistantService;
        private readonly IMapper _mapper;
        private readonly IBaseService _baseService;

        public NewsHitManager(INewsHitAssistantService newsHitAssistantService, IMapper mapper, IBaseService baseService)
        {
            _newsHitAssistantService = newsHitAssistantService;
            _mapper = mapper;
            _baseService = baseService;
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<NewsHitDto>>> GetList()
        {
            return new SuccessDataResult<List<NewsHitDto>>(await _newsHitAssistantService.GetList());
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<NewsHitDto>>> GetListByNewsId(int newsId)
        {
            return new SuccessDataResult<List<NewsHitDto>>(await _newsHitAssistantService.GetListByNewsId(newsId));
        }

        [ValidationAspect(typeof(NewsHitAddDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("INewsHitService.Get")]
        public async Task<IResult> AddWithDetail(NewsHitAddDto dto)
        {
            var data = _mapper.Map<NewsHitDetailAddDto>(dto);
            data.IpAddress = _baseService.UserIpAddress;
            data.UserId = _baseService.RequestUserId > 0 ? _baseService.RequestUserId : (int?)null;
            await _newsHitAssistantService.AddWithDetail(data);
            return new SuccessResult(Messages.Added);
        }
    }
}

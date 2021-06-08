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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class NewsPositionManager : INewsPositionService
    {
        private readonly INewsPositionAssistantService _newsPositionAssistantService;
        private readonly IMapper _mapper;

        public NewsPositionManager(INewsPositionAssistantService newsPositionAssistantService, IMapper mapper)
        {
            _newsPositionAssistantService = newsPositionAssistantService;
            _mapper = mapper;
        }

        [SecuredOperation("NewsPositionUpdate")]
        [ValidationAspect(typeof(NewsPositionValidator))]
        [LogAspect()]
        [CacheRemoveAspect("INewsPositionService.Get")]
        [CacheRemoveAspect("INewsService.Get")]
        [CacheRemoveAspect("IMainPageService.Get")]
        [CacheRemoveAspect("INewsDetailPageService.Get")]
        public async Task<IResult> UpdateNewsPositionOrders(List<NewsPositionUpdateDto> newsPositions)
        {
            await _newsPositionAssistantService.UpdateNewsPositionOrders(newsPositions);
            return new SuccessResult(Messages.Updated);
        }


        [SecuredOperation("NewsPositionGet")]
        [PerformanceAspect()]
        public async Task<IDataResult<List<NewsPositionDto>>> GetOrdersByNewsPositionEntityId(int newsPositionEntityId, int limit)
        {
            return new SuccessDataResult<List<NewsPositionDto>>(await _newsPositionAssistantService.GetOrdersByNewsPositionEntityId(newsPositionEntityId, limit));
        }

    }
}

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
    public class SubscriptionManager : ISubscriptionService
    {
        private readonly ISubscriptionAssistantService _subscriptionAssistantService;
        private readonly IMapper _mapper;

        public SubscriptionManager(ISubscriptionAssistantService subscriptionAssistantService, IMapper mapper)
        {
            _subscriptionAssistantService = subscriptionAssistantService;
            _mapper = mapper;
        }

        [SecuredOperation("SubscriptionGet")]
        [PerformanceAspect()]
        public IDataResult<List<SubscriptionDto>> GetListByPaging(SubscriptionPagingDto pagingDto, out int total)
        {
            return new SuccessDataResult<List<SubscriptionDto>>(_subscriptionAssistantService.GetListByPaging(pagingDto, out total));
        }

        [SecuredOperation("SubscriptionGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<SubscriptionDto>>> GetList()
        {
            return new SuccessDataResult<List<SubscriptionDto>>(await _subscriptionAssistantService.GetList());
        }

        [SecuredOperation("SubscriptionGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<SubscriptionDto>> GetById(int subscriptionId)
        {
            var data = await _subscriptionAssistantService.GetViewById(subscriptionId);
            if (data == null)
            {
                return new ErrorDataResult<SubscriptionDto>(Messages.RecordNotFound);
            }
            return new SuccessDataResult<SubscriptionDto>(data);
        }

        [ValidationAspect(typeof(SubscriptionAddDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("ISubscriptionService.Get")]
        public async Task<IResult> Add(SubscriptionAddDto subscriptionAddDto)
        {
            var subscription = _mapper.Map<Subscription>(subscriptionAddDto);
            await _subscriptionAssistantService.Add(subscription);
            return new SuccessResult(Messages.Added);
        }
    }
}

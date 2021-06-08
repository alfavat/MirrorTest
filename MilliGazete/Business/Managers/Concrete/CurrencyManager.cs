using AutoMapper;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Managers.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class CurrencyManager : ICurrencyService
    {
        private readonly ICurrencyAssistantService _currencyAssistantService;
        private readonly IMapper _mapper;

        public CurrencyManager(ICurrencyAssistantService currencyAssistantService, IMapper mapper)
        {
            _currencyAssistantService = currencyAssistantService;
            _mapper = mapper;
        }

        [SecuredOperation("CurrencyAdd")]
        [ValidationAspect(typeof(CurrencyAddDtoValidator))]
        [CacheRemoveAspect("ICurrencyService.Get")]
        public async Task<IResult> AddArray(List<CurrencyAddDto> list)
        {
            foreach (var item in list)
            {
                var currency = await _currencyAssistantService.GetByShortKey(item.ShortKey);
                var data = _mapper.Map(item, currency);
                if (currency == null)
                {
                    await _currencyAssistantService.Add(data);
                }
                else
                {
                    await _currencyAssistantService.Update(data);
                }
            }
            return new SuccessResult(Messages.Added);
        }

        [CacheAspect(duration: 1440)]
        public async Task<IDataResult<List<CurrencyDto>>> GetList()
        {
            return new SuccessDataResult<List<CurrencyDto>>(await _currencyAssistantService.GetList());
        }
    }
}
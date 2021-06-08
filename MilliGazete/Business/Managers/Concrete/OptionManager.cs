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
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class OptionManager : IOptionService
    {
        private readonly IOptionAssistantService _optionAssistantService;
        private readonly IMapper _mapper;

        public OptionManager(IOptionAssistantService OptionAssistantService, IMapper mapper)
        {
            _optionAssistantService = OptionAssistantService;
            _mapper = mapper;
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<OptionDto>> Get()
        {
            var data = await _optionAssistantService.GetView();
            if (data == null)
            {
                return new ErrorDataResult<OptionDto>(Messages.RecordNotFound);
            }
            return new SuccessDataResult<OptionDto>(data);
        }

        [SecuredOperation("OptionUpdate")]
        [ValidationAspect(typeof(OptionUpdateDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("IOptionService.Get")]
        public async Task<IResult> Update(OptionUpdateDto optionUpdateDto)
        {
            var data = await _optionAssistantService.Get();
            if (data == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            var opt = _mapper.Map(optionUpdateDto, data);
            await _optionAssistantService.Update(opt);
            return new SuccessResult(Messages.Updated);
        }
    }
}

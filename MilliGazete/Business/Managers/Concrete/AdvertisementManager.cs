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
    public class AdvertisementManager : IAdvertisementService
    {
        private readonly IAdvertisementAssistantService _advertisementAssistantService;
        private readonly IMapper _mapper;

        public AdvertisementManager(IAdvertisementAssistantService advertisementAssistantService, IMapper mapper)
        {
            _advertisementAssistantService = advertisementAssistantService;
            _mapper = mapper;
        }

        [PerformanceAspect()]
        [CacheAspect()]
        public async Task<IDataResult<List<AdvertisementDto>>> GetActiveList()
        {
            return new SuccessDataResult<List<AdvertisementDto>>(await _advertisementAssistantService.GetActiveList());
        }

        [SecuredOperation("AdvertisementGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<AdvertisementDto>>> GetList()
        {
            return new SuccessDataResult<List<AdvertisementDto>>(await _advertisementAssistantService.GetList());
        }

        [SecuredOperation("AdvertisementGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<AdvertisementDto>> GetById(int id)
        {
            var Advertisement = await _advertisementAssistantService.GetById(id);
            if (Advertisement == null)
            {
                return new ErrorDataResult<AdvertisementDto>(Messages.RecordNotFound);
            }
            var data = _mapper.Map<AdvertisementDto>(Advertisement);
            return new SuccessDataResult<AdvertisementDto>(data);
        }

        [SecuredOperation("AdvertisementUpdate")]
        [ValidationAspect(typeof(AdvertisementUpdateDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("IAdvertisementService.Get")]
        public async Task<IResult> Update(AdvertisementUpdateDto dto)
        {
            var data = await _advertisementAssistantService.GetById(dto.Id);
            if (data == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            var cto = _mapper.Map(dto, data);
            await _advertisementAssistantService.Update(cto);
            return new SuccessResult(Messages.Updated);
        }

        [SecuredOperation("AdvertisementAdd")]
        [ValidationAspect(typeof(AdvertisementAddDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("IAdvertisementService.Get")]
        public async Task<IResult> Add(AdvertisementAddDto dto)
        {
            var data = _mapper.Map<Advertisement>(dto);
            await _advertisementAssistantService.Add(data);
            return new SuccessResult(Messages.Added);
        }

        [SecuredOperation("AdvertisementDelete")]
        [LogAspect()]
        [CacheRemoveAspect("IAdvertisementService.Get")]
        public async Task<IResult> Delete(int id)
        {
            var data = await _advertisementAssistantService.GetById(id);
            if (data == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            data.Deleted = true;
            await _advertisementAssistantService.Update(data);
            return new SuccessResult(Messages.Deleted);
        }

        [SecuredOperation("AdvertisementUpdate")]
        [LogAspect()]
        [CacheRemoveAspect("IAdvertisementService.Get")]
        public async Task<IResult> ChangeActiveStatus(ChangeActiveStatusDto changeActiveStatusDto)
        {
            var data = await _advertisementAssistantService.GetById(changeActiveStatusDto.Id);
            if (data == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            data.Active = changeActiveStatusDto.Active;
            await _advertisementAssistantService.Update(data);
            return new SuccessResult(Messages.Updated);
        }
    }
}

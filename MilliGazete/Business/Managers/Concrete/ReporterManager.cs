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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class ReporterManager : IReporterService
    {
        private readonly IReporterAssistantService _reporterAssistantService;
        private readonly IMapper _mapper;

        public ReporterManager(IReporterAssistantService reporterAssistantService, IMapper mapper)
        {
            _reporterAssistantService = reporterAssistantService;
            _mapper = mapper;
        }

        [PerformanceAspect()]
        public async Task<IDataResult<List<ReporterDto>>> GetList()
        {
            return new SuccessDataResult<List<ReporterDto>>(await _reporterAssistantService.GetList());
        }

        [SecuredOperation("ReporterGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<ReporterDto>> GetById(int id)
        {
            var data = await _reporterAssistantService.GetViewById(id);
            if (data == null)
            {
                return new ErrorDataResult<ReporterDto>(Messages.RecordNotFound);
            }
            return new SuccessDataResult<ReporterDto>(data);
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<ReporterDto>> GetByUrl(string url)
        {
            if (url.StringIsNullOrEmpty())
            {
                return new ErrorDataResult<ReporterDto>(Messages.EmptyParameter);
            }
            var data = await _reporterAssistantService.GetViewByUrl(url);
            if (data == null)
            {
                return new ErrorDataResult<ReporterDto>(Messages.RecordNotFound);
            }
            return new SuccessDataResult<ReporterDto>(data);
        }

        [SecuredOperation("ReporterUpdate")]
        [ValidationAspect(typeof(ReporterUpdateDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("INewsPositionService.Get")]
        [CacheRemoveAspect("INewsService.Get")]
        [CacheRemoveAspect("IMainPageService.Get")]
        [CacheRemoveAspect("INewsDetailPageService.Get")]
        [CacheRemoveAspect("IReporterService.Get")]
        public async Task<IResult> Update(ReporterUpdateDto dto)
        {
            var data = await _reporterAssistantService.GetById(dto.Id);
            if (data == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            var cto = _mapper.Map(dto, data);
            await _reporterAssistantService.Update(cto);
            return new SuccessResult(Messages.Updated);
        }

        [SecuredOperation("ReporterAdd")]
        [ValidationAspect(typeof(ReporterAddDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("INewsPositionService.Get")]
        [CacheRemoveAspect("INewsService.Get")]
        [CacheRemoveAspect("IMainPageService.Get")]
        [CacheRemoveAspect("INewsDetailPageService.Get")]
        [CacheRemoveAspect("IReporterService.Get")]
        public async Task<IDataResult<int>> Add(ReporterAddDto dto)
        {
            var data = _mapper.Map<Reporter>(dto);
            var result = await _reporterAssistantService.Add(data);
            return new SuccessDataResult<int>(result.Id, Messages.Added);
        }

        [SecuredOperation("ReporterDelete")]
        [LogAspect()]
        [CacheRemoveAspect("INewsPositionService.Get")]
        [CacheRemoveAspect("INewsService.Get")]
        [CacheRemoveAspect("IMainPageService.Get")]
        [CacheRemoveAspect("INewsDetailPageService.Get")]
        [CacheRemoveAspect("IReporterService.Get")]
        public async Task<IResult> Delete(int id)
        {
            var data = await _reporterAssistantService.GetById(id);
            if (data == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            await _reporterAssistantService.Delete(data);
            return new SuccessResult(Messages.Deleted);
        }

        [PerformanceAspect()]
        public async Task<IDataResult<Tuple<List<NewsPagingViewDto>, int>>> GetListByPagingViaUrl(ReporterNewsPagingDto pagingDto)
        {
            if (pagingDto.Url.StringIsNullOrEmpty())
            {
                return new ErrorDataResult<Tuple<List<NewsPagingViewDto>, int>>(Messages.EmptyParameter);
            }
            return new SuccessDataResult<Tuple<List<NewsPagingViewDto>, int>>(await _reporterAssistantService.GetListByPagingViaUrl(pagingDto));
        }

    }
}

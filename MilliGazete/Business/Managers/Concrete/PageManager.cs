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
    public class PageManager : IPageService
    {
        private readonly IPageAssistantService _pageAssistantService;
        private readonly IMapper _mapper;

        public PageManager(IPageAssistantService PageAssistantService, IMapper mapper)
        {
            _pageAssistantService = PageAssistantService;
            _mapper = mapper;
        }

        [SecuredOperation("PageGet")]
        [PerformanceAspect()]
        public IDataResult<List<PageDto>> GetListByPaging(PagePagingDto pagingDto, out int total)
        {
            return new SuccessDataResult<List<PageDto>>(_pageAssistantService.GetListByPaging(pagingDto, out total));
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<PageDto>>> GetList()
        {
            return new SuccessDataResult<List<PageDto>>(await _pageAssistantService.GetList());
        }

        [SecuredOperation("PageGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<PageDto>> GetById(int id)
        {
            var data = await _pageAssistantService.GetViewById(id);
            if (data == null)
            {
                return new ErrorDataResult<PageDto>(Messages.RecordNotFound);
            }
            return new SuccessDataResult<PageDto>(data);
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<PageDto>> GetByUrl(string url)
        {
            var data = await _pageAssistantService.GetByUrl(url);
            if (data == null)
            {
                return new ErrorDataResult<PageDto>(Messages.RecordNotFound);
            }
            return new SuccessDataResult<PageDto>(data);
        }

        [SecuredOperation("PageUpdate")]
        [ValidationAspect(typeof(PageUpdateDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("IPageService.Get")]
        public async Task<IResult> Update(PageUpdateDto dto)
        {
            var data = await _pageAssistantService.GetById(dto.Id);
            if (data == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            var cto = _mapper.Map(dto, data);
            await _pageAssistantService.Update(cto);
            return new SuccessResult(Messages.Updated);
        }

        [SecuredOperation("PageAdd")]
        [ValidationAspect(typeof(PageAddDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("IPageService.Get")]
        public async Task<IResult> Add(PageAddDto dto)
        {
            var data = _mapper.Map<Page>(dto);
            await _pageAssistantService.Add(data);
            return new SuccessResult(Messages.Added);
        }

        [SecuredOperation("PageDelete")]
        [LogAspect()]
        [CacheRemoveAspect("IPageService.Get")]
        public async Task<IResult> Delete(int id)
        {
            var data = await _pageAssistantService.GetById(id);
            if (data == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            data.Deleted = true;
            await _pageAssistantService.Update(data);
            return new SuccessResult(Messages.Deleted);
        }
    }
}

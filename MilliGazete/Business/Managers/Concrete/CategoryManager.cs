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
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryAssistantService _categoryAssistantService;
        private readonly IMapper _mapper;

        public CategoryManager(ICategoryAssistantService categoryAssistantService, IMapper mapper)
        {
            _categoryAssistantService = categoryAssistantService;
            _mapper = mapper;
        }

        [SecuredOperation("CategoryGet")]
        [PerformanceAspect()]
        public IDataResult<List<CategoryDto>> GetListByPaging(CategoryPagingDto pagingDto, out int total)
        {
            return new SuccessDataResult<List<CategoryDto>>(_categoryAssistantService.GetListByPaging(pagingDto, out total));
        }

        [SecuredOperation("CategoryGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<CategoryDto>>> GetList()
        {
            return new SuccessDataResult<List<CategoryDto>>(await _categoryAssistantService.GetList());
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<CategoryDto>>> GetActiveList()
        {
            return new SuccessDataResult<List<CategoryDto>>(await _categoryAssistantService.GetActiveList());
        }

        [SecuredOperation("CategoryGet")]
        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<CategoryDto>> GetById(int categoryId)
        {
            var data = await _categoryAssistantService.GetViewById(categoryId);
            if (data == null)
            {
                return new ErrorDataResult<CategoryDto>(Messages.RecordNotFound);
            }
            return new SuccessDataResult<CategoryDto>(data);
        }

        [SecuredOperation("CategoryUpdate")]
        [ValidationAspect(typeof(CategoryUpdateDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("IMainPageService.Get")]
        [CacheRemoveAspect("INewsDetailPageService.Get")]
        [CacheRemoveAspect("INewsService.Get")]
        [CacheRemoveAspect("ISearchPageService.Get")]
        [CacheRemoveAspect("ICategoryPageService.Get")]
        [CacheRemoveAspect("ICategoryService.Get")]
        [CacheRemoveAspect("IReporterService.Get")]
        public async Task<IResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            var category = await _categoryAssistantService.GetById(categoryUpdateDto.Id);
            if (category == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            var cto = _mapper.Map(categoryUpdateDto, category);
            await _categoryAssistantService.Update(cto);
            return new SuccessResult(Messages.Updated);
        }

        [SecuredOperation("CategoryAdd")]
        [ValidationAspect(typeof(CategoryAddDtoValidator))]
        [LogAspect()]
        [CacheRemoveAspect("IMainPageService.Get")]
        [CacheRemoveAspect("INewsDetailPageService.Get")]
        [CacheRemoveAspect("INewsService.Get")]
        [CacheRemoveAspect("ISearchPageService.Get")]
        [CacheRemoveAspect("ICategoryPageService.Get")]
        [CacheRemoveAspect("ICategoryService.Get")]
        [CacheRemoveAspect("IReporterService.Get")]
        public async Task<IResult> Add(CategoryAddDto categoryAddDto)
        {
            var category = _mapper.Map<Category>(categoryAddDto);
            await _categoryAssistantService.Add(category);
            return new SuccessResult(Messages.Added);
        }

        [SecuredOperation("CategoryDelete")]
        [LogAspect()]
        [CacheRemoveAspect("IMainPageService.Get")]
        [CacheRemoveAspect("INewsDetailPageService.Get")]
        [CacheRemoveAspect("INewsService.Get")]
        [CacheRemoveAspect("ISearchPageService.Get")]
        [CacheRemoveAspect("ICategoryPageService.Get")]
        [CacheRemoveAspect("ICategoryService.Get")]
        [CacheRemoveAspect("IReporterService.Get")]
        public async Task<IResult> Delete(int categoryId)
        {
            var category = await _categoryAssistantService.GetById(categoryId);
            if (category == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            category.Deleted = true;
            await _categoryAssistantService.Update(category);
            return new SuccessResult(Messages.Deleted);
        }

        [SecuredOperation("CategoryUpdate")]
        [LogAspect()]
        [CacheRemoveAspect("IMainPageService.Get")]
        [CacheRemoveAspect("INewsDetailPageService.Get")]
        [CacheRemoveAspect("INewsService.Get")]
        [CacheRemoveAspect("ISearchPageService.Get")]
        [CacheRemoveAspect("ICategoryPageService.Get")]
        [CacheRemoveAspect("ICategoryService.Get")]
        [CacheRemoveAspect("IReporterService.Get")]
        public async Task<IResult> ChangeActiveStatus(ChangeActiveStatusDto changeActiveStatusDto)
        {
            var category = await _categoryAssistantService.GetById(changeActiveStatusDto.Id);
            if (category == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            category.Active = changeActiveStatusDto.Active;
            await _categoryAssistantService.Update(category);
            return new SuccessResult(Messages.Updated);
        }
    }
}

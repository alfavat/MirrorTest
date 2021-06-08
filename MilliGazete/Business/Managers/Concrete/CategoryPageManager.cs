using AutoMapper;
using Business.Constants;
using Business.Managers.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class CategoryPageManager : ICategoryPageService
    {
        private readonly ICategoryPageAssistantService _categoryPageAssistantService;
        private readonly ICategoryAssistantService _categoryAssistantService;
        private readonly IMapper _mapper;

        public CategoryPageManager(ICategoryPageAssistantService categoryPageAssistantService, ICategoryAssistantService categoryAssistantService, IMapper mapper)
        {
            _categoryPageAssistantService = categoryPageAssistantService;
            _categoryAssistantService = categoryAssistantService;
            _mapper = mapper;
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<CategoryDto>> GetByUrl(string url)
        {
            var category = await _categoryAssistantService.GetByUrl(url);
            if (category == null)
            {
                return new ErrorDataResult<CategoryDto>(Messages.RecordNotFound);
            }
            var data = _mapper.Map<CategoryDto>(category);
            return new SuccessDataResult<CategoryDto>(data);
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<CategoryDto>> GetById(int id)
        {
            var data = await _categoryAssistantService.GetViewById(id);
            if (data == null)
            {
                return new ErrorDataResult<CategoryDto>(Messages.RecordNotFound);
            }
            return new SuccessDataResult<CategoryDto>(data);
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<MainHeadingDto>>> GetLastMainHeadingNewsByCategoryUrl(string url, int limit)
        {
            return new SuccessDataResult<List<MainHeadingDto>>(await _categoryPageAssistantService.GetTopMainHeadingNewsByCategoryUrl(url, limit));
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<MainPageCategoryNewsDto>>> GetLastNewsByCategoryId(int id, int limit)
        {
            return new SuccessDataResult<List<MainPageCategoryNewsDto>>(await _categoryPageAssistantService.GetLastNewsByCategoryId(id, limit));
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<MainHeadingDto>>> GetLastMainHeadingNewsByCategoryId(int id, int limit)
        {
            return new SuccessDataResult<List<MainHeadingDto>>(await _categoryPageAssistantService.GetTopMainHeadingNewsByCategoryId(id, limit));
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<MainPageCategoryNewsDto>>> GetLastNewsByCategoryUrl(string url, int limit)
        {
            return new SuccessDataResult<List<MainPageCategoryNewsDto>>(await _categoryPageAssistantService.GetLastNewsByCategoryUrl(url, limit));
        }
    }
}

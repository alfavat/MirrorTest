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
    public class NewsBookmarkManager : INewsBookmarkService
    {
        private readonly INewsBookmarkAssistantService _newsBookmarkAssistantService;
        private readonly IMapper _mapper;
        private readonly INewsAssistantService _newsAssistantService;
        private readonly IBaseService _baseService;

        public NewsBookmarkManager(INewsBookmarkAssistantService NewsBookmarkAssistantService, IMapper mapper, INewsAssistantService newsAssistantService, IBaseService baseService)
        {
            _newsBookmarkAssistantService = NewsBookmarkAssistantService;
            _mapper = mapper;
            _newsAssistantService = newsAssistantService;
            _baseService = baseService;
        }

        [SecuredOperation("")]
        [PerformanceAspect()]
        public async Task<IDataResult<NewsBookmarkDto>> GetByNewsUrl(string url)
        {
            return new SuccessDataResult<NewsBookmarkDto>(await _newsBookmarkAssistantService.GetByNewsUrl(url, _baseService.RequestUserId));
        }

        [SecuredOperation("")]
        [PerformanceAspect()]
        public async Task<IDataResult<List<NewsBookmarkDto>>> GetList()
        {
            return new SuccessDataResult<List<NewsBookmarkDto>>(await _newsBookmarkAssistantService.GetList(_baseService.RequestUserId));
        }

        [SecuredOperation("")]
        [PerformanceAspect()]
        public async Task<IDataResult<NewsBookmarkDto>> GetByNewsId(int newsId)
        {
            var data = await _newsBookmarkAssistantService.GetByNewsId(newsId, _baseService.RequestUserId);
            if (data == null)
            {
                return new ErrorDataResult<NewsBookmarkDto>(Messages.RecordNotFound);
            }
            return new SuccessDataResult<NewsBookmarkDto>(data);
        }

        [SecuredOperation("")]
        [ValidationAspect(typeof(NewsBookmarkAddValidator))]
        [LogAspect()]
        [CacheRemoveAspect("INewsBookmarkService.Get")]
        [CacheRemoveAspect("INewsDetailPageService.Get")]
        public async Task<IResult> Add(NewsBookmarkAddDto dto)
        {
            var news = await _newsAssistantService.GetById(dto.NewsId);
            if (news == null)
            {
                return new ErrorResult(Messages.RecordNotFound);
            }
            var data = _mapper.Map<NewsBookmark>(dto);
            data.UserId = _baseService.RequestUserId;
            await _newsBookmarkAssistantService.Add(data);
            return new SuccessResult(Messages.Added);
        }

        [SecuredOperation("")]
        [LogAspect()]
        [CacheRemoveAspect("INewsBookmarkService.Get")]
        [CacheRemoveAspect("INewsDetailPageService.Get")]
        public async Task<IResult> DeleteByNewsId(int newsId)
        {
            await _newsBookmarkAssistantService.DeleteByNewsId(newsId, _baseService.RequestUserId);
            return new SuccessResult(Messages.Deleted);
        }

        [SecuredOperation("")]
        [LogAspect()]
        public async Task<IDataResult<bool>> HasUserBookmarkedNews(int newsId)
        {
            return new SuccessDataResult<bool>(await _newsBookmarkAssistantService.HasUserBookmarkedNews(newsId, _baseService.RequestUserId));
        }
    }
}

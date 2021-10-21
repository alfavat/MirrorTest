using Business.Managers.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class NewsDetailPageManager : INewsDetailPageService
    {
        private readonly INewsDetailPageAssistantService _newsDetailPageAssistantService;
        private readonly IBaseService _baseService;

        public NewsDetailPageManager(INewsDetailPageAssistantService newsDetailPageAssistantService , IBaseService baseService)
        {
            _newsDetailPageAssistantService = newsDetailPageAssistantService;
            _baseService = baseService;
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<MostViewedNewsDto>>> GetLastWeekMostViewedNews(int limit)
        {
            return new SuccessDataResult<List<MostViewedNewsDto>>(await _newsDetailPageAssistantService.GetLastWeekMostViewedNews(limit));
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<MostSharedNewsDto>>> GetMostShareNewsList(int limit)
        {
            return new SuccessDataResult<List<MostSharedNewsDto>>(await _newsDetailPageAssistantService.GetMostShareNewsList(limit));
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<NewsDetailPageDto>> GetNewsWithDetails(string url)
        {
            return new SuccessDataResult<NewsDetailPageDto>(await _newsDetailPageAssistantService.GetNewsWithDetails(url, preview: url.GetPreviewFromUrl(),requestedUserId: _baseService.RequestUserId));
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<NewsDetailPageDto>> GetNewsWithDetailsById(int id, bool preview = false)
        {
            return new SuccessDataResult<NewsDetailPageDto>(await _newsDetailPageAssistantService.GetNewsWithDetails("", id, preview ,requestedUserId: _baseService.RequestUserId));
        }

        [PerformanceAspect()]
        public async Task<IDataResult<Tuple<List<NewsDetailPageDto>, int>>> GetNewsWithDetailsByPaging(MainPageNewsPagingDto pagingDto)
        {
            return new SuccessDataResult<Tuple<List<NewsDetailPageDto>, int>>(await _newsDetailPageAssistantService.GetNewsWithDetailsByPaging(pagingDto));
        }


    }
}

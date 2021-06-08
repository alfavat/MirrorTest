using Business.Helpers.Abstract;
using Business.Managers.Abstract;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class SearchPageManager : ISearchPageService
    {
        private readonly ISearchPageAssistantService _searchPageAssistantService;

        private readonly INewsHelper _newsHelper;

        public SearchPageManager(ISearchPageAssistantService searchPageAssistantService, INewsHelper newsHelper)
        {
            _searchPageAssistantService = searchPageAssistantService;
            _newsHelper = newsHelper;
        }

        [PerformanceAspect()]
        public async Task<IDataResult<List<MainPageTagNewsDto>>> GetNewsByTagUrl(string url, int limit)
        {
            return new SuccessDataResult<List<MainPageTagNewsDto>>(await _searchPageAssistantService.GetNewsByTagUrl(url, limit));
        }

        [PerformanceAspect()]
        public IDataResult<List<MainPageSearchNewsDto>> GetListByPaging(NewsPagingDto pagingDto, out int total)
        {
            return new SuccessDataResult<List<MainPageSearchNewsDto>>(_searchPageAssistantService.GetNewsListByPaging(pagingDto, out total));
        }
    }
}

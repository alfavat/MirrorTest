using Business.Managers.Abstract;
using DataAccess.Abstract;
using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class NewsDetailPageAssistantManager : INewsDetailPageAssistantService
    {
        private readonly INewsDal _newsDal;
        private readonly IBaseService _baseService;

        public NewsDetailPageAssistantManager(INewsDal NewsDal, IBaseService baseService)
        {
            _newsDal = NewsDal;
            _baseService = baseService;
        }

        public async Task<List<MostViewedNewsDto>> GetLastWeekMostViewedNews(int limit)
        {
            return await _newsDal.GetLastWeekMostViewedNews(limit);
        }

        public async Task<List<MostSharedNewsDto>> GetMostShareNewsList(int limit)
        {
            return await _newsDal.GetMostShareNewsList(limit);
        }

        public async Task<NewsDetailPageDto> GetNewsWithDetails(string url, int? id = null, bool preview = false, int? requestedUserId = null)
        {
            return await _newsDal.GetNewsWithDetails(url, id, preview, requestedUserId);
        }

        public async Task<Tuple<List<NewsDetailPageDto>, int>> GetNewsWithDetailsByPaging(MainPageNewsPagingDto pagingDto, int? requestedUserId = null)
        {
            return await _newsDal.GetNewsWithDetailsByPaging(pagingDto, requestedUserId);
        }
    }
}

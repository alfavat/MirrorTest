using AutoMapper;
using Business.Managers.Abstract;
using DataAccess.Abstract;
using Entity.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class NewsDetailPageAssistantManager : INewsDetailPageAssistantService
    {
        private readonly INewsDal _newsDal;
        private readonly IBaseService _baseService;
        private readonly IMapper _mapper;

        public NewsDetailPageAssistantManager(INewsDal NewsDal, IBaseService baseService, IMapper mapper)
        {
            _newsDal = NewsDal;
            _baseService = baseService;
            _mapper = mapper;
        }

        public async Task<List<MostViewedNewsDto>> GetLastWeekMostViewedNews(int limit)
        {
            return await _newsDal.GetLastWeekMostViewedNews(limit);
        }

        public async Task<List<MostSharedNewsDto>> GetMostShareNewsList(int limit)
        {
            return await _newsDal.GetMostShareNewsList(limit);
        }

        public async Task<NewsDetailPageDto> GetNewsWithDetails(string url, int? id = null, bool preview = false)
        {
            return await _newsDal.GetNewsWithDetails(url, id, preview, _baseService.RequestUserId == 0 ? null : _baseService.RequestUserId);
        }

        public async Task<Tuple<List<NewsDetailPageDto>, int>> GetNewsWithDetailsByPaging(MainPageNewsPagingDto pagingDto)
        {
            return await _newsDal.GetNewsWithDetailsByPaging(pagingDto, _baseService.RequestUserId == 0 ? null : _baseService.RequestUserId);
        }
        public async Task<Tuple<List<NewsDetailPagePagingDto>, int>> GetNewsWithDetailsByPaging2(MainPageNewsPagingDto pagingDto)
        {
            DateTime fromDate = DateTime.Now.AddDays(-1 * AppSettingsExtension.GetValue<int>("NewsWithDetailsByPagingDays"));
            var query = _newsDal.GetActiveList();

            if (pagingDto.Query.StringNotNullOrEmpty())
                query = query.Where(f => f.Title.Contains(pagingDto.Query));

            if (!pagingDto.NewsId.HasValue)
            {
                var historyNo = pagingDto.Url.GetHistoryNoFromUrl();
                pagingDto.NewsId = await query.Where(g => g.HistoryNo == historyNo).Select(f => f.Id).FirstOrDefaultAsync();
            }
            var data = query.OrderBy(pagingDto.OrderBy).Skip((pagingDto.PageNumber - 1) * pagingDto.Limit.CheckLimit()).Take(pagingDto.Limit.CheckLimit());
            var list = await _mapper.ProjectTo<NewsDetailPagePagingDto>(data).ToListAsync();
            var total = query.Count();
            return new Tuple<List<NewsDetailPagePagingDto>, int>(list, total);
        }

    }
}

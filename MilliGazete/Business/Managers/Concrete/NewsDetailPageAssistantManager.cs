using AutoMapper;
using Business.Managers.Abstract;
using DataAccess.Abstract;
using Entity.Dtos;
using Entity.Enums;
using Entity.Models;
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
        private readonly IMapper _mapper;
        private readonly INewsDal _newsDal;
        private readonly INewsTagDal _newsTagDal;
        private readonly INewsCategoryDal _newsCategoryDal;

        public NewsDetailPageAssistantManager(INewsDal NewsDal, INewsTagDal newsTagDal, INewsCategoryDal newsCategoryDal, IMapper mapper)
        {
            _newsDal = NewsDal;
            _newsTagDal = newsTagDal;
            _newsCategoryDal = newsCategoryDal;
            _mapper = mapper;
        }

        public async Task<List<MostViewedNewsDto>> GetLastWeekMostViewedNews(int limit)
        {
            //DateTime from = DateTime.Now.AddDays(-7), to = DateTime.Now;
            var query = _newsDal.GetActiveList()//.Where(f => f.PublishDate >= from && f.PublishDate <= to)
                .Include(f => f.NewsFile).ThenInclude(f => f.File)
                .Include(f => f.NewsCounter)
                .Include(f => f.NewsPosition)
                .Include(f => f.NewsCategory).ThenInclude(f => f.Category)
                .AsQueryable();//.OrderByDescending(f => f.NewsCounter.OrderByDescending(g => g.Value).First(f => f.CounterEntityId == (int)NewsCounterEntities.TotalViewCount).Value).Take(limit.CheckLimit());
            var res = await _mapper.ProjectTo<MostViewedNewsDto>(query).ToListAsync();
            return res.OrderByDescending(f => f.ViewCount).Take(limit.CheckLimit()).ToList();
        }

        public async Task<List<MostSharedNewsDto>> GetMostShareNewsList(int limit)
        {
            var query = _newsDal.GetActiveList()
                .Include(f => f.NewsFile).ThenInclude(f => f.File)
                .Include(f => f.NewsCounter)
                .Include(f => f.NewsPosition)
                .Include(f => f.NewsCategory).ThenInclude(f => f.Category)
                .AsQueryable();//.OrderByDescending(f => f.NewsCounter.OrderByDescending(g => g.Value).First(f => f.CounterEntityId == (int)NewsCounterEntities.TotalShareCount).Value).Take(limit.CheckLimit());
            var res = await _mapper.ProjectTo<MostSharedNewsDto>(query).ToListAsync();
            return res.OrderByDescending(f => f.ShareCount).Take(limit.CheckLimit()).ToList();
        }


        public async Task<NewsDetailPageDto> GetNewsWithDetails(string url, int? id = null, bool preview = false, int? requestedUserId = null)
        {
            var newList = preview ? _newsDal.GetList(f => !f.Deleted) : _newsDal.GetActiveList();
            var activeList = newList
            .Include(f => f.NewsRelatedNewsNews).ThenInclude(f => f.RelatedNews).ThenInclude(f => f.NewsFile).ThenInclude(f => f.File)
            .Include(f => f.NewsRelatedNewsNews).ThenInclude(f => f.RelatedNews).ThenInclude(f => f.NewsCategory).ThenInclude(f => f.Category)
            .Include(f => f.NewsTag).ThenInclude(f => f.Tag)
            .Include(f => f.NewsProperty)
            .Include(f => f.NewsBookmark)
            .Include(f => f.NewsPosition)
            .Include(f => f.NewsCategory).ThenInclude(f => f.Category)
            .Include(f => f.NewsFile).ThenInclude(f => f.File)
            .Include(f => f.NewsFile).ThenInclude(f => f.VideoCoverFile);

            News item = null;
            if (id.HasValue)
            {
                item = await  activeList.FirstOrDefaultAsync(f => f.Id == id.Value);
            }
            else
            {
                var historyNo = url.GetHistoryNoFromUrl();
                item = await activeList.FirstOrDefaultAsync(f => f.HistoryNo == historyNo);
            }

            var data = _mapper.Map<NewsDetailPageDto>(item);
            if (data != null)
                data.BookMarkStatus = requestedUserId.HasValue && item.NewsBookmark.Any(f => f.UserId == requestedUserId);
            return data;
        }

        public List<NewsDetailPageDto> GetNewsWithDetailsByPaging(MainPageNewsPagingDto pagingDto, out int total, int? requestedUserId = null)
        {
            var query = _newsDal.GetActiveList()
                .Include(f => f.NewsRelatedNewsNews).ThenInclude(f => f.RelatedNews).ThenInclude(f => f.NewsFile).ThenInclude(f => f.File)
                .Include(f => f.NewsRelatedNewsNews).ThenInclude(f => f.RelatedNews).ThenInclude(f => f.NewsCategory).ThenInclude(f => f.Category)
                .Include(f => f.NewsTag).ThenInclude(f => f.Tag)
                .Include(f => f.NewsProperty)
                .Include(f => f.NewsPosition)
                .Include(f => f.NewsBookmark)
                .Include(f => f.NewsCategory).ThenInclude(f => f.Category)
                .Include(f => f.NewsFile).ThenInclude(f => f.File)
                .Include(f => f.NewsFile).ThenInclude(f => f.VideoCoverFile).AsQueryable();

            if (pagingDto.Query.StringNotNullOrEmpty())
                query = query.Where(f => f.Title.Contains(pagingDto.Query));


            News item = null;
            if (pagingDto.NewsId.HasValue)
            {
                item = query.FirstOrDefault(f => f.Id == pagingDto.NewsId.Value);
            }
            else
            {
                var historyNo = pagingDto.Url.GetHistoryNoFromUrl();
                item = query.FirstOrDefault(f => f.HistoryNo == historyNo);
            }

            if (item != null)
            {
                var tagIds = item.NewsTag.Select(y => y.TagId);
                var tagNewsIds = _newsTagDal.GetList(f => tagIds.Contains(f.TagId)).Select(f => f.NewsId);

                var categoryIds = item.NewsCategory.Select(y => y.CategoryId);
                var categoryNewsIds = _newsCategoryDal.GetList(f => categoryIds.Contains(f.CategoryId)).Select(f => f.NewsId);

                var relatedNewsIds = item.NewsRelatedNewsNews.Select(u => u.NewsId);
                query = query.Where(f => (relatedNewsIds.Contains(f.Id) || tagNewsIds.Contains(f.Id) || categoryNewsIds.Contains(f.Id)) && f.Id != item.Id);
            }
            total = query.Count();
            var list = query.OrderBy(pagingDto.OrderBy).Skip((pagingDto.PageNumber - 1) * pagingDto.Limit.CheckLimit()).Take(pagingDto.Limit.CheckLimit()).ToList();
            var data = _mapper.Map<List<NewsDetailPageDto>>(list);
            data.ForEach(f =>
            {
                f.BookMarkStatus = requestedUserId.HasValue && requestedUserId > 0 && list.FirstOrDefault(u => u.Id == f.Id).NewsBookmark.Any(t => t.UserId == requestedUserId);
            });
            return data;
        }
    }
}

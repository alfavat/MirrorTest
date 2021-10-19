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
    public class MainPageAssistantManager : IMainPageAssistantService
    {
        private readonly IMapper _mapper;
        private readonly INewsDal _newsDal;
        private readonly INewsTagDal _newsTagDal;
        private readonly INewsCategoryDal _newsCategoryDal;

        public MainPageAssistantManager(INewsDal NewsDal, INewsTagDal newsTagDal, INewsCategoryDal newsCategoryDal, IMapper mapper)
        {
            _newsDal = NewsDal;
            _newsTagDal = newsTagDal;
            _newsCategoryDal = newsCategoryDal;
            _mapper = mapper;
        }

        public async Task<NewsDetailPageDto> GetNewsWithDetails(string url)
        {
            int historyNo = url.GetHistoryNoFromUrl();

            var item = await _newsDal.GetActiveList()
                .Include(f => f.Author)
                .Include(f => f.NewsRelatedNewsNews).ThenInclude(f => f.RelatedNews).ThenInclude(f => f.NewsFiles).ThenInclude(f => f.File)
                .Include(f => f.NewsRelatedNewsNews).ThenInclude(f => f.RelatedNews).ThenInclude(f => f.NewsCategories).ThenInclude(f => f.Category)
                .Include(f => f.NewsTags).ThenInclude(f => f.Tag)
                .Include(f => f.NewsProperties)
                .Include(f => f.NewsPositions)
                .Include(f => f.NewsCategories).ThenInclude(f => f.Category)
                .Include(f => f.NewsFiles).ThenInclude(f => f.File)
                .Include(f => f.NewsFiles).ThenInclude(f => f.VideoCoverFile)
                .FirstOrDefaultAsync(f => f.HistoryNo == historyNo);

            return _mapper.Map<NewsDetailPageDto>(item);
        }

        public async Task<Tuple<List<NewsDetailPageDto>, int>> GetNewsWithDetailsByPaging(MainPageNewsPagingDto pagingDto)
        {
            var query = _newsDal.GetActiveList()
                .Include(f => f.Author)
                .Include(f => f.NewsPositions)
                .Include(f => f.NewsRelatedNewsNews)
                .Include(f => f.NewsTags).ThenInclude(f => f.Tag)
                .Include(f => f.NewsProperties)
                .Include(f => f.NewsCategories).ThenInclude(f => f.Category)
                .Include(f => f.NewsFiles).ThenInclude(f => f.File)
                .Include(f => f.NewsFiles).ThenInclude(f => f.VideoCoverFile).AsQueryable();

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
                var tagIds = item.NewsTags.Select(y => y.TagId);
                var tagNewsIds = _newsTagDal.GetList(f => tagIds.Contains(f.TagId)).Select(f => f.NewsId);

                var categoryIds = item.NewsCategories.Select(y => y.CategoryId);
                var categoryNewsIds = _newsCategoryDal.GetList(f => categoryIds.Contains(f.CategoryId)).Select(f => f.NewsId);

                var relatedNewsIds = item.NewsRelatedNewsNews.Select(u => u.NewsId);
                query = query.Where(f => (relatedNewsIds.Contains(f.Id) || tagNewsIds.Contains(f.Id) || categoryNewsIds.Contains(f.Id)) && f.Id != item.Id);
            }

            var total = query.Count();
            var list = _mapper.ProjectTo<NewsDetailPageDto>(query);
            var data = await list.OrderBy(pagingDto.OrderBy).Skip((pagingDto.PageNumber - 1) * pagingDto.Limit.CheckLimit()).Take(pagingDto.Limit.CheckLimit()).ToListAsync();
            return new Tuple<List<NewsDetailPageDto>, int>(data, total);
        }

        public async Task<List<BreakingNewsDto>> GetTopBreakingNews(int limit)
        {
            var query = _newsDal.GetActiveList()
                .Include(f => f.Author)
                .Include(f => f.NewsCategories).ThenInclude(f => f.Category)
                .Include(f => f.NewsPositions).AsQueryable();
            query = query.Where(u => u.NewsPositions.Any(u => u.PositionEntityId == (int)NewsPositionEntities.BreakingNews));
            query = query.OrderByDescending(u => u.PublishDate).ThenByDescending(f => f.PublishTime).Take(limit.CheckLimit());
            return await _mapper.ProjectTo<BreakingNewsDto>(query).ToListAsync();
        }

        public async Task<List<WideHeadingNewsDto>> GetTopWideHeadingNews(int limit)
        {
            var query = _newsDal.GetActiveList()
             .Include(f => f.Author)
             .Include(f => f.NewsCategories).ThenInclude(f => f.Category)
             .Include(f => f.NewsPositions).AsQueryable();
            query = query.Where(u => u.NewsPositions.Any(u => u.PositionEntityId == (int)NewsPositionEntities.MainPageWideHeadingNews && u.Order > 0));
            query = query.OrderBy(u => u.NewsPositions.First(u => u.PositionEntityId == (int)NewsPositionEntities.MainPageWideHeadingNews).Order).Take(limit.CheckLimit());
            return await _mapper.ProjectTo<WideHeadingNewsDto>(query).ToListAsync();
        }

        public async Task<List<SubHeadingDto>> GetTopSubHeadingNews(int limit)
        {
            var query = _newsDal.GetActiveList()
               .Include(f => f.Author)
               .Include(f => f.NewsCategories).ThenInclude(f => f.Category)
               .Include(f => f.NewsPositions)
               .AsQueryable();
            query = query.Where(u => u.NewsPositions.Any(u => u.PositionEntityId == (int)NewsPositionEntities.SubHeadingNews && u.Order > 0));
            query = query.OrderBy(u => u.NewsPositions.First(u => u.PositionEntityId == (int)NewsPositionEntities.SubHeadingNews).Order).Take(limit.CheckLimit());
            return await _mapper.ProjectTo<SubHeadingDto>(query).ToListAsync();
        }

        public async Task<List<SubHeadingDto>> GetTopSubHeadingNews2(int limit)
        {
            var query = _newsDal.GetActiveList().Include(f => f.Author).AsQueryable();
            query = query.Include(f => f.NewsCategories).ThenInclude(f => f.Category).Include(f => f.NewsPositions);
            query = query.Where(u => u.NewsPositions.Any(u => u.PositionEntityId == (int)NewsPositionEntities.SubHeadingNews2 && u.Order > 0));
            query = query.OrderBy(u => u.NewsPositions.First(u => u.PositionEntityId == (int)NewsPositionEntities.SubHeadingNews2).Order).Take(limit.CheckLimit());
            return await _mapper.ProjectTo<SubHeadingDto>(query).ToListAsync();
        }

        public async Task<List<MainHeadingDto>> GetTopMainHeadingNews(int limit)
        {
            var query = _newsDal.GetActiveList().Include(f => f.Author).AsQueryable();
            query = query.Include(f => f.NewsCategories).ThenInclude(f => f.Category).Include(f => f.NewsPositions);
            query = query.Where(u => u.NewsPositions.Any(u => u.PositionEntityId == (int)NewsPositionEntities.MainHeadingNews && u.Order > 0));
            query = query.OrderBy(u => u.NewsPositions.First(u => u.PositionEntityId == (int)NewsPositionEntities.MainHeadingNews).Order).Take(limit.CheckLimit());
            return await _mapper.ProjectTo<MainHeadingDto>(query).ToListAsync();
        }

        public async Task<List<MainPageNewsDto>> GetTopMainPageNews(int limit)
        {
            var query = _newsDal.GetActiveList().Include(f => f.Author).AsQueryable();
            query = query.Include(f => f.NewsCategories).ThenInclude(f => f.Category).Include(f => f.NewsPositions);
            query = query.Where(u => u.NewsPositions.Any(u => u.PositionEntityId == (int)NewsPositionEntities.MainPageNews && u.Order > 0));
            query = query.OrderBy(u => u.NewsPositions.First(u => u.PositionEntityId == (int)NewsPositionEntities.MainPageNews).Order).Take(limit.CheckLimit());
            return await _mapper.ProjectTo<MainPageNewsDto>(query).ToListAsync();
        }

        public async Task<List<MainPageVideoNewsDto>> GetLastVideoNews(int limit)
        {
            var query = _newsDal.GetActiveList().Include(f => f.Author).AsQueryable();
            query = query.Include(f => f.NewsCategories).ThenInclude(f => f.Category).Include(f => f.NewsPositions)
                .Include(f => f.NewsFiles).ThenInclude(f => f.File)
                .Include(f => f.NewsFiles).ThenInclude(f => f.VideoCoverFile);
            query = query.Where(u => u.NewsTypeEntityId == (int)NewsTypeEntities.VideoGallery);
            query = query.OrderByDescending(u => u.PublishDate).ThenByDescending(f => f.PublishTime).Take(limit.CheckLimit());
            var res = _mapper.ProjectTo<MainPageVideoNewsDto>(query);
            return await res.ToListAsync();
        }

        public async Task<List<MainPageImageNewsDto>> GetLastImageNews(int limit)
        {
            var query = _newsDal.GetActiveList().Include(f => f.Author).AsQueryable();
            query = query.Include(f => f.NewsCategories).ThenInclude(f => f.Category).Include(f => f.NewsPositions);
            query = query.Where(u => u.NewsTypeEntityId == (int)NewsTypeEntities.ImageGallery);
            query = query.OrderByDescending(u => u.PublishDate).ThenByDescending(f => f.PublishTime).Take(limit.CheckLimit());
            return await _mapper.ProjectTo<MainPageImageNewsDto>(query).ToListAsync();
        }

        public async Task<List<MainPageStoryNewsDto>> GetStoryNews(int limit)
        {
            var query = _newsDal.GetActiveList()
                .Include(f => f.NewsCategories).ThenInclude(f => f.Category)
                .Include(f => f.NewsPositions)
                .Include(f => f.NewsFiles).ThenInclude(f => f.File)
                .Include(f => f.Author).AsQueryable();

            query = query.Where(u => //u.PublishDate.Value.Date >= DateTime.Now.AddDays(-7).Date && 
            u.NewsPositions.Any(u => u.PositionEntityId == (int)NewsPositionEntities.MobileStory && u.Order > 0));
            query = query.Where(f => f.NewsFiles.Any(g => g.NewsFileTypeEntityId == (int)NewsFileTypeEntities.StoryThumbnailImage));
            query = query.Where(f => f.NewsFiles.Any(g => g.NewsFileTypeEntityId == (int)NewsFileTypeEntities.StoryBigImage));
            query = query.OrderBy(u => u.NewsPositions.First(u => u.PositionEntityId == (int)NewsPositionEntities.MobileStory).Order);

            var res = await query.ToListAsync();
            var cateoryList = res.GroupBy(f => f.NewsCategories.First().Category.CategoryName);
            var list = new List<MainPageStoryNewsDto>();
            foreach (var item in cateoryList.AsEnumerable())
            {
                list.Add(new MainPageStoryNewsDto
                {
                    CategoryName = item.Key,
                    Thumbnail = item.OrderByDescending(f => f.PublishDate).ThenByDescending(f => f.PublishTime)
                    .First().NewsFiles.First(g => g.NewsFileTypeEntityId == (int)NewsFileTypeEntities.StoryThumbnailImage).File.FileName.GetFullFilePath(),
                    Data = item.Select(t => new StoryNewsDetail
                    {
                        NewsId = t.Id,
                        StoryDate = t.PublishDate.Value,
                        StoryTime = t.PublishTime.Value,
                        MainImage = t.NewsFiles.First(g => g.NewsFileTypeEntityId == (int)NewsFileTypeEntities.StoryBigImage).File.FileName.GetFullFilePath(),
                        Title = t.Title,
                        Url = t.Url.GetUrl(t.HistoryNo, t.NewsTypeEntityId, t.NewsCategories.Select(e => e.Category.Url).FirstOrDefault())
                    }).Take(limit).ToList()
                });
            }

            return list;
        }

        public async Task<List<MainPageFourHillNewsDto>> GetTopMainPageFourHillNews(int limit)
        {
            var query = _newsDal.GetActiveList().Include(f => f.Author).AsQueryable();
            query = query.Include(f => f.NewsCategories).ThenInclude(f => f.Category).Include(f => f.NewsPositions);
            query = query.Where(u => u.NewsPositions.Any(u => u.PositionEntityId == (int)NewsPositionEntities.MainPageFourHill && u.Order > 0));
            query = query.OrderBy(u => u.NewsPositions.First(u => u.PositionEntityId == (int)NewsPositionEntities.MainPageFourHill).Order).Take(limit.CheckLimit());
            return await _mapper.ProjectTo<MainPageFourHillNewsDto>(query).ToListAsync();
        }

        public async Task<List<MainPageArticleDto>> GetLastArticles(int limit)
        {
            var query = _newsDal.GetActiveList()
                .Include(f => f.NewsCategories).ThenInclude(f => f.Category)
                .Include(f => f.NewsPositions)
                .Include(f => f.Author).ThenInclude(f => f.PhotoFile)
                .AsQueryable();
            query = query.Where(u => u.NewsTypeEntityId == (int)NewsTypeEntities.Article && u.AuthorId != null);
            query = query.OrderByDescending(u => u.PublishDate).ThenByDescending(f => f.PublishTime).Take(limit.CheckLimit());
            return await _mapper.ProjectTo<MainPageArticleDto>(query).ToListAsync();
        }
    }
}

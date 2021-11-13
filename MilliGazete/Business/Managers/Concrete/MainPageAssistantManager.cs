using AutoMapper;
using Business.Managers.Abstract;
using DataAccess.Abstract;
using Entity.Dtos;
using Entity.Enums;
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

        public MainPageAssistantManager(INewsDal NewsDal, IMapper mapper)
        {
            _newsDal = NewsDal;
            _mapper = mapper;
        }

        public async Task<List<BreakingNewsDto>> GetTopBreakingNews(int limit)
        {
            var languageId = CommonHelper.CurrentLanguageId;
            var query = _newsDal.GetActiveList()
                .Where(u => u.NewsPositions.Any(u => u.PositionEntityId == (int)NewsPositionEntities.BreakingNews) &&
                u.NewsCategories.Any(f => languageId == 0 || f.Category.LanguageId == languageId))
                .OrderByDescending(u => u.PublishDate).ThenByDescending(f => f.PublishTime).Take(limit.CheckLimit())
                .Include(f => f.Author)
                .Include(f => f.NewsCategories).ThenInclude(f => f.Category)
                .Include(f => f.NewsPositions).AsQueryable();

            return await _mapper.ProjectTo<BreakingNewsDto>(query).ToListAsync();
        }

        public async Task<List<FlashNewsDto>> GetLastFlashNews(int limit)
        {
            var languageId = CommonHelper.CurrentLanguageId;
            var minutes = AppSettingsExtension.GetValue<int>("FlashNewsMinutes");
            var dt = DateTime.Now.AddMinutes(-1 * minutes);
            var query = _newsDal.GetActiveList()
                .Where(u => u.NewsPositions.Any(u => u.PositionEntityId == (int)NewsPositionEntities.FlashNews) &&
                u.PublishDate != null && u.PublishTime != null &&
                u.PublishDate.Value.Date >= dt.Date && u.PublishTime.Value >= dt.TimeOfDay &&
                u.NewsCategories.Any(f => languageId == 0 || f.Category.LanguageId == languageId))
                .OrderByDescending(u => u.PublishDate).ThenByDescending(f => f.PublishTime).Take(limit.CheckLimit())
                .Include(f => f.Author)
                .Include(f => f.NewsCategories).ThenInclude(f => f.Category)
                .Include(f => f.NewsPositions).AsQueryable();

            return await _mapper.ProjectTo<FlashNewsDto>(query).ToListAsync();
        }

        public async Task<List<WideHeadingNewsDto>> GetTopWideHeadingNews(int limit)
        {
            var languageId = CommonHelper.CurrentLanguageId;
            var query = _newsDal.GetActiveList()
             .Where(u => u.NewsPositions.Any(u => u.PositionEntityId == (int)NewsPositionEntities.MainPageWideHeadingNews && u.Order > 0) &&
             u.NewsCategories.Any(f => languageId == 0 || f.Category.LanguageId == languageId))
             .OrderBy(u => u.NewsPositions.First(u => u.PositionEntityId == (int)NewsPositionEntities.MainPageWideHeadingNews).Order).Take(limit.CheckLimit())
             .Include(f => f.Author)
             .Include(f => f.NewsCategories).ThenInclude(f => f.Category)
             .Include(f => f.NewsPositions).AsQueryable();
            return await _mapper.ProjectTo<WideHeadingNewsDto>(query).ToListAsync();
        }

        public async Task<List<SubHeadingDto>> GetTopSubHeadingNews(int limit)
        {
            var languageId = CommonHelper.CurrentLanguageId;
            var query = _newsDal.GetActiveList()
                .Where(u => u.NewsPositions.Any(u => u.PositionEntityId == (int)NewsPositionEntities.SubHeadingNews && u.Order > 0) &&
                u.NewsCategories.Any(f => languageId == 0 || f.Category.LanguageId == languageId))
                .OrderBy(u => u.NewsPositions.First(u => u.PositionEntityId == (int)NewsPositionEntities.SubHeadingNews).Order).Take(limit.CheckLimit())
               .Include(f => f.Author)
               .Include(f => f.NewsCategories).ThenInclude(f => f.Category)
               .Include(f => f.NewsPositions)
               .AsQueryable();
            return await _mapper.ProjectTo<SubHeadingDto>(query).ToListAsync();
        }

        public async Task<List<SubHeadingDto>> GetTopSubHeadingNews2(int limit)
        {
            var languageId = CommonHelper.CurrentLanguageId;
            var query = _newsDal.GetActiveList()
                .Where(u => u.NewsPositions.Any(u => u.PositionEntityId == (int)NewsPositionEntities.SubHeadingNews2 && u.Order > 0) &&
                u.NewsCategories.Any(f => languageId == 0 || f.Category.LanguageId == languageId))
                .OrderBy(u => u.NewsPositions.First(u => u.PositionEntityId == (int)NewsPositionEntities.SubHeadingNews2).Order).Take(limit.CheckLimit())
                .Include(f => f.Author)
                .Include(f => f.NewsCategories).ThenInclude(f => f.Category).Include(f => f.NewsPositions)
                .AsQueryable();
            return await _mapper.ProjectTo<SubHeadingDto>(query).ToListAsync();
        }

        public async Task<List<MainHeadingDto>> GetTopMainHeadingNews(int limit)
        {
            var languageId = CommonHelper.CurrentLanguageId;
            var query = _newsDal.GetActiveList()
                .Where(u => u.NewsPositions.Any(u => u.PositionEntityId == (int)NewsPositionEntities.MainHeadingNews && u.Order > 0) &&
                 u.NewsCategories.Any(f => languageId == 0 || f.Category.LanguageId == languageId))
                .OrderBy(u => u.NewsPositions.First(u => u.PositionEntityId == (int)NewsPositionEntities.MainHeadingNews).Order).Take(limit.CheckLimit())
                .Include(f => f.Author)
                .Include(f => f.NewsCategories).ThenInclude(f => f.Category)
                .Include(f => f.NewsPositions);
            return await _mapper.ProjectTo<MainHeadingDto>(query).ToListAsync();
        }

        public async Task<List<MainPageNewsDto>> GetTopMainPageNews(int limit)
        {
            var languageId = CommonHelper.CurrentLanguageId;
            var query = _newsDal.GetActiveList()
                .Where(u => u.NewsPositions.Any(u => u.PositionEntityId == (int)NewsPositionEntities.MainPageNews && u.Order > 0) &&
                 u.NewsCategories.Any(f => languageId == 0 || f.Category.LanguageId == languageId))
                .OrderBy(u => u.NewsPositions.First(u => u.PositionEntityId == (int)NewsPositionEntities.MainPageNews).Order).Take(limit.CheckLimit())
                .Include(f => f.Author)
                .Include(f => f.NewsCategories).ThenInclude(f => f.Category)
                .Include(f => f.NewsPositions);
            return await _mapper.ProjectTo<MainPageNewsDto>(query).ToListAsync();
        }

        public async Task<List<MainPageVideoNewsDto>> GetLastVideoNews(int limit)
        {
            var languageId = CommonHelper.CurrentLanguageId;
            var query = _newsDal.GetActiveList()
                .Where(u => u.NewsTypeEntityId == (int)NewsTypeEntities.VideoGallery &&
                 u.NewsCategories.Any(f => languageId == 0 || f.Category.LanguageId == languageId))
                .OrderByDescending(u => u.PublishDate).ThenByDescending(f => f.PublishTime).Take(limit.CheckLimit())
                .Include(f => f.Author)
                .Include(f => f.NewsCategories).ThenInclude(f => f.Category)
                .Include(f => f.NewsPositions)
                .Include(f => f.NewsFiles).ThenInclude(f => f.File)
                .Include(f => f.NewsFiles).ThenInclude(f => f.VideoCoverFile);
            var res = _mapper.ProjectTo<MainPageVideoNewsDto>(query);
            return await res.ToListAsync();
        }

        public async Task<List<MainPageImageNewsDto>> GetLastImageNews(int limit)
        {
            var languageId = CommonHelper.CurrentLanguageId;
            var query = _newsDal.GetActiveList()
                .Where(u => u.NewsTypeEntityId == (int)NewsTypeEntities.ImageGallery &&
                 u.NewsCategories.Any(f => languageId == 0 || f.Category.LanguageId == languageId))
                .OrderByDescending(u => u.PublishDate).ThenByDescending(f => f.PublishTime).Take(limit.CheckLimit())
                .Include(f => f.Author)
                .Include(f => f.NewsCategories).ThenInclude(f => f.Category)
                .Include(f => f.NewsPositions);
            return await _mapper.ProjectTo<MainPageImageNewsDto>(query).ToListAsync();
        }

        public async Task<List<MainPageStoryNewsDto>> GetStoryNews(int limit)
        {
            var languageId = CommonHelper.CurrentLanguageId;
            var query = _newsDal.GetActiveList()
                .Where(u => u.NewsPositions.Any(u => u.PositionEntityId == (int)NewsPositionEntities.MobileStory && u.Order > 0) &&
                 u.NewsCategories.Any(f => languageId == 0 || f.Category.LanguageId == languageId))
                .Include(f => f.NewsCategories).ThenInclude(f => f.Category)
                .Include(f => f.NewsPositions)
                .Include(f => f.NewsFiles).ThenInclude(f => f.File)
                .Include(f => f.Author).AsQueryable();

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
            var languageId = CommonHelper.CurrentLanguageId;
            var query = _newsDal.GetActiveList()
                .Where(u => u.NewsPositions.Any(u => u.PositionEntityId == (int)NewsPositionEntities.MainPageFourHill && u.Order > 0) &&
                 u.NewsCategories.Any(f => languageId == 0 || f.Category.LanguageId == languageId))
                .OrderBy(u => u.NewsPositions.First(u => u.PositionEntityId == (int)NewsPositionEntities.MainPageFourHill).Order).Take(limit.CheckLimit())
                .Include(f => f.Author)
                .Include(f => f.NewsCategories).ThenInclude(f => f.Category)
                .Include(f => f.NewsPositions);
            return await _mapper.ProjectTo<MainPageFourHillNewsDto>(query).ToListAsync();
        }

        public async Task<List<MainPageArticleDto>> GetLastArticles(int limit)
        {
            var languageId = CommonHelper.CurrentLanguageId;
            var query = _newsDal.GetActiveList()
                .Where(u => u.NewsTypeEntityId == (int)NewsTypeEntities.Article && u.AuthorId != null &&
                 u.NewsCategories.Any(f => languageId == 0 || f.Category.LanguageId == languageId))
                .OrderByDescending(u => u.PublishDate).ThenByDescending(f => f.PublishTime).Take(limit.CheckLimit())
                .Include(f => f.NewsCategories).ThenInclude(f => f.Category)
                .Include(f => f.NewsPositions)
                .Include(f => f.Author).ThenInclude(f => f.PhotoFile)
                .AsQueryable();
            return await _mapper.ProjectTo<MainPageArticleDto>(query).ToListAsync();
        }
    }
}

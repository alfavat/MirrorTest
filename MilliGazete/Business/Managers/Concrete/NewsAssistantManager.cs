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
    public class NewsAssistantManager : INewsAssistantService
    {
        private readonly INewsDal _newsDal;
        private readonly IFileAssistantService _fileAssistantService;
        private readonly IMapper _mapper;
        private readonly IBaseService _baseService;
        public NewsAssistantManager(INewsDal newsDal, IFileAssistantService fileAssistantService, IMapper mapper, IBaseService baseService)
        {
            _newsDal = newsDal;
            _mapper = mapper;
            _fileAssistantService = fileAssistantService;
            _baseService = baseService;
        }

        public async Task<Tuple<List<NewsPagingViewDto>, int>> GetListByPaging(NewsPagingDto pagingDto)
        {
            int languageId = (int)_baseService.UserLanguage;
            var query = _newsDal.GetList(f => !f.Deleted && f.IsLastNews)
                .Include(f => f.NewsTags).ThenInclude(f => f.Tag)
                .Include(f => f.NewsCategories).ThenInclude(f => f.Category)
                .Include(f => f.NewsHitDetails)
                .AsQueryable();
            query = query.Where(prop => prop.NewsCategories.Any(prop => (languageId == 0 || prop.Category.LanguageId == languageId)));

            if (pagingDto.CategoryIds.HasValue())
            {
                query = query.Where(f => f.NewsCategories.Any(g => pagingDto.CategoryIds.Contains(g.CategoryId) && !g.Category.Deleted));
            }

            if (pagingDto.Query.StringNotNullOrEmpty())
                query = query.Where(f => /*f.Url.ToLower().Contains(pagingDto.Query.ToLower()) ||*/
                /*f.SeoDescription.ToLower().Contains(pagingDto.Query.ToLower()) || f.SeoKeywords.ToLower().Contains(pagingDto.Query.ToLower()) ||*/
                /*f.HtmlContent.ToLower().Contains(pagingDto.Query.ToLower()) || f.SeoTitle.ToLower().Contains(pagingDto.Query.ToLower()) ||*/
                f.ShortDescription.ToLower().Contains(pagingDto.Query.ToLower()) || //f.SocialDescription.ToLower().Contains(pagingDto.Query.ToLower()) ||
                 /*f.SocialTitle.ToLower().Contains(pagingDto.Query.ToLower()) ||*/ f.Title.ToLower().Contains(pagingDto.Query.ToLower()));

            if (pagingDto.Active.HasValue)
                query = query.Where(f => f.Active == pagingDto.Active.Value);

            if (pagingDto.Approved.HasValue)
                query = query.Where(f => f.Approved == pagingDto.Approved.Value);

            if (pagingDto.IsDraft.HasValue)
                query = query.Where(f => f.IsDraft == pagingDto.IsDraft.Value);

            if (pagingDto.NewsAgencyEntityId.HasValue)
                query = query.Where(f => f.NewsAgencyEntityId == pagingDto.NewsAgencyEntityId.Value);

            if (pagingDto.NewsTypeEntityId.HasValue)
                query = query.Where(f => f.NewsTypeEntityId == pagingDto.NewsTypeEntityId.Value);

            if (pagingDto.FromPublishedAt.HasValue && pagingDto.ToPublishedAt.HasValue)
                query = query.Where(f => f.PublishDate >= pagingDto.FromPublishedAt.Value && f.PublishDate <= pagingDto.ToPublishedAt.Value);

            if (pagingDto.UserId.HasValue)
                query = query.Where(f => f.AddUserId == pagingDto.UserId);

            if (pagingDto.AuthorId.HasValue)
                query = query.Where(f => f.AuthorId == pagingDto.AuthorId);

            var total = await query.CountAsync();
            var mapped = _mapper.ProjectTo<NewsPagingViewDto>(query);
            var data = await mapped.OrderBy(pagingDto.OrderBy).Skip((pagingDto.PageNumber - 1) * pagingDto.Limit.CheckLimit()).Take(pagingDto.Limit.CheckLimit()).ToListAsync();
            return new Tuple<List<NewsPagingViewDto>, int>(data, total);

        }

        public async Task<News> GetById(int newsId) => await _newsDal.Get(p => p.Id == newsId && !p.Deleted);

        public async Task<NewsViewDto> GetViewById(int newsId)
        {
            var languageId = (int)_baseService.UserLanguage;
            var data = await _newsDal.GetView(p => p.Id == newsId && !p.Deleted && p.NewsCategories.Any(f => (languageId == 0 || f.Category.LanguageId == languageId)));
            return _mapper.Map<NewsViewDto>(data);
        }

        public async Task<NewsViewDto> GetViewByUrl(string url)
        {
            var languageId = (int)_baseService.UserLanguage;
            var historyNo = url.GetHistoryNoFromUrl();
            var data = await _newsDal.GetView(p => (p.HistoryNo == historyNo || p.Url == url) &&
            p.NewsCategories.Any(f => languageId == 0 || f.Category.LanguageId == languageId) &&
            !p.Deleted && p.Active && p.Approved.Value && !p.IsDraft && p.IsLastNews);
            return _mapper.Map<NewsViewDto>(data);
        }

        public async Task Update(News news) => await _newsDal.Update(news);

        public async Task<int> Add(NewsAddDto newsAddDto, int addUserId, int historyNo)
        {
            if (newsAddDto.HtmlContent.StringNotNullOrEmpty())
            {
                newsAddDto.HtmlContent = newsAddDto.HtmlContent.Replace("<img>", $"<img src='' alt='' />");
            }
            var news = _mapper.Map<News>(newsAddDto);
            news.HistoryNo = historyNo;
            news.IsLastNews = true;
            if (newsAddDto.NewsId > 0)
            {
                news.AddUserId = addUserId;
            }
            var categories = _mapper.Map<List<NewsCategory>>(newsAddDto.NewsCategoryList);
            var files = _mapper.Map<List<NewsFile>>(newsAddDto.NewsFileList);
            files = await CopyPoolImages(files);
            var positions = _mapper.Map<List<NewsPosition>>(newsAddDto.NewsPositionList);
            var properties = _mapper.Map<List<NewsProperty>>(newsAddDto.NewsPropertyList);
            var relatedNews = _mapper.Map<List<NewsRelatedNews>>(newsAddDto.NewsRelatedNewsList);
            var tags = _mapper.Map<List<NewsTag>>(newsAddDto.NewsTagList);
            await _newsDal.AddNewsWithDetails(news, categories, files, positions, properties,
                relatedNews, tags, newsAddDto.NewsId == 0);
            return news.Id;
        }

        public async Task<List<NewsFile>> CopyPoolImages(List<NewsFile> newsFiles)
        {
            var poolImages = newsFiles.Where(prop => prop.CameFromPool).ToList();
            newsFiles.RemoveAll(prop => poolImages.Any(p => p.FileId == prop.FileId));
            if (poolImages.Any())
            {
                foreach (var image in poolImages)
                {
                    var newFileId = await _fileAssistantService.CopyFile(image.FileId);
                    image.FileId = newFileId;
                    newsFiles.Add(image);
                }
            }
            return newsFiles;
        }

        public async Task<List<NewsHistoryDto>> GetListByHistoryNo(int historyNo)
        {
            var languageId = (int)_baseService.UserLanguage;
            var list = _newsDal.GetList(p => !p.Deleted && p.HistoryNo == historyNo && p.NewsCategories.Any(f => (languageId == 0 || f.Category.LanguageId == languageId)))
                .Include(p => p.NewsCategories).ThenInclude(p => p.Category)
                .OrderByDescending(f => f.CreatedAt);
            return await _mapper.ProjectTo<NewsHistoryDto>(list).ToListAsync();
        }

        public async Task<int> GetMaxHistoryNo() => await _newsDal.GetList(f => !f.Deleted).MaxAsync(f => f.HistoryNo) ?? 0;

        public async Task<List<NewsSiteMapDto>> GetListForSiteMap()
        {
            var list = _newsDal.GetActiveList()
                .Include(f => f.NewsCategories).ThenInclude(f => f.Category)
                .Include(f => f.NewsAgencyEntity)
                .AsQueryable()
                .OrderByDescending(f => f.PublishDate).ThenBy(f => f.PublishTime);

            var data = await _mapper.ProjectTo<NewsItem>(list).ToListAsync();
            var groupedByCategory = data.GroupBy(f => f.CategoryUrl).ToList();
            var res = new List<NewsSiteMapDto>();
            foreach (var item in groupedByCategory)
            {
                res.Add(new NewsSiteMapDto
                {
                    CategoryName = item.Key,
                    Items = item.GroupBy(x => new { Month = x.PulishDate.Month, Year = x.PulishDate.Year })
                      .Select(r => new GroupByDateDto
                      {
                          NewsList = r.ToList(),
                          YearAndMonth = r.Key.Year + "-" + r.Key.Month.ToString().PadLeft(2, '0')
                      }).ToList()
                });
            }
            return res;

        }

        public async Task IncreaseShareCount(int newsId) => await _newsDal.IncreaseShareCount(newsId);

        public async Task<List<ArticleDto>> GetLastWeekMostViewedArticles(int limit)
        {
            var query = _newsDal.GetActiveList()
                .Include(f => f.Author).ThenInclude(f => f.PhotoFile)
                .Include(f => f.NewsFiles).ThenInclude(f => f.File)
                .Include(f => f.NewsCounters)
                .Include(f => f.NewsCategories).ThenInclude(f => f.Category)
                .Where(p => p.NewsTypeEntityId == (int)NewsTypeEntities.Article && p.AuthorId != null)
                .AsQueryable();

            return await _mapper.ProjectTo<ArticleDto>(query).OrderByDescending(f => f.ReadCount).Take(limit.CheckLimit()).ToListAsync();
        }

        public async Task<List<NewsViewDto>> GetListByAuthorId(int authorId)
        {
            var languageId = (int)_baseService.UserLanguage;
            var list = _newsDal.GetActiveList().Where(p => p.AuthorId == authorId && p.NewsCategories.Any(f => (languageId == 0 || f.Category.LanguageId == languageId)))
                .Include(f => f.NewsTags).ThenInclude(f => f.Tag)
                .Include(f => f.NewsCategories).ThenInclude(f => f.Category)
                .Include(f => f.NewsAgencyEntity)
                .Include(f => f.Author).ThenInclude(f => f.PhotoFile)
                .Include(f => f.NewsTypeEntity)
                .Include(f => f.NewsFiles).ThenInclude(f => f.File)
                .Include(f => f.NewsFiles).ThenInclude(f => f.VideoCoverFile)
                .Include(f => f.NewsRelatedNewsNews).ThenInclude(f => f.RelatedNews).ThenInclude(f => f.NewsFiles).ThenInclude(f => f.File)
                .Include(f => f.NewsPositions).Include(f => f.NewsProperties)
                .AsQueryable();
            return await _mapper.ProjectTo<NewsViewDto>(list).ToListAsync();

        }

        public async Task<ArticleDto> GetArticleByUrl(string url)
        {
            var languageId = (int)_baseService.UserLanguage;
            var historyNo = url.GetHistoryNoFromUrl();
            var item = await _newsDal.GetActiveList()
                .Include(f => f.Author).ThenInclude(f => f.PhotoFile)
                .Include(f => f.NewsFiles).ThenInclude(f => f.File)
                .Include(f => f.NewsCounters)
                .FirstOrDefaultAsync(p => p.NewsTypeEntityId == (int)NewsTypeEntities.Article && p.AuthorId != null &&
                (p.HistoryNo == historyNo || p.Url == url) &&
                p.NewsCategories.Any(f => languageId == 0 || f.Category.LanguageId == languageId));

            return item != null ? _mapper.Map<ArticleDto>(item) : null;
        }

    }
}

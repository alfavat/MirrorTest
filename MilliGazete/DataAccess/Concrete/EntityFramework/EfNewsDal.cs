using DataAccess.Abstract;
using DataAccess.Base;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entity.Dtos;
using Entity.Enums;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfNewsDal : EfEntityRepositoryBase<News>, INewsDal
    {
        public EfNewsDal(MilliGazeteDbContext milligazeteDb) : base(milligazeteDb)
        {
        }
        private string GetUrl2(int? NewsTypeEntityId, string Url, int? HistoryNo, string CategoryUrl)
        {

            if (NewsTypeEntityId == (int)NewsTypeEntities.Article)
            {
                return "/makale/" + Url + "-" + HistoryNo.ToString();
            }
            return "/" + CategoryUrl + "/" + Url + "-" + HistoryNo.ToString();
        }

        public async Task<Tuple<List<NewsDetailPageDto>, int>> GetNewsWithDetailsByPaging(MainPageNewsPagingDto pagingDto, int? requestedUserId = null)
        {
            DateTime fromDate = DateTime.Now.AddDays(-1 * AppSettingsExtension.GetValue<int>("NewsWithDetailsByPagingDays"));
            var query = GetActiveList();

            if (pagingDto.Query.StringNotNullOrEmpty())
                query = query.Where(f => f.Title.Contains(pagingDto.Query));

            if (!pagingDto.NewsId.HasValue)
            {
                var historyNo = pagingDto.Url.GetHistoryNoFromUrl();
                pagingDto.NewsId = await query.Where(g => g.HistoryNo == historyNo).Select(f => f.Id).FirstOrDefaultAsync();
            }
            if (query != null)
            {
                if (pagingDto.NewsId.HasValue)
                {
                    var tagIds = Db.NewsTags.Where(f => f.NewsId == pagingDto.NewsId.Value).Select(y => y.TagId);
                    var tagNewsIds = Db.NewsTags.Where(f => tagIds.Contains(f.TagId) && f.NewsId != pagingDto.NewsId.Value).Select(f => f.NewsId);

                    var categoryIds = Db.NewsCategories.Where(f => f.NewsId == pagingDto.NewsId.Value).Select(y => y.CategoryId);
                    var categoryNewsIds = Db.NewsCategories.Where(f => categoryIds.Contains(f.CategoryId) && f.NewsId != pagingDto.NewsId.Value).Select(f => f.NewsId);

                    var relatedNewsIds = Db.NewsRelatedNews.Where(f => f.NewsId == pagingDto.NewsId.Value).Select(u => u.NewsId);
                    query = query.Where(f => f.PublishDate >= fromDate && (relatedNewsIds.Contains(f.Id) || tagNewsIds.Contains(f.Id) || categoryNewsIds.Contains(f.Id)) && f.Id != pagingDto.NewsId.Value);
                }
                var list = await query.OrderBy(pagingDto.OrderBy).Skip((pagingDto.PageNumber - 1) * pagingDto.Limit.CheckLimit()).Take(pagingDto.Limit.CheckLimit())
                    .Select(f => new NewsDetailPageDto
                    {
                        HtmlContent = f.HtmlContent,
                        Id = f.Id,
                        InnerTitle = f.InnerTitle,
                        NewsAgencyEntityId = f.NewsAgencyEntityId,
                        NewsTypeEntityId = f.NewsTypeEntityId,
                        PublishDate = f.PublishDate,
                        PublishTime = f.PublishTime,
                        SeoDescription = f.SeoDescription,
                        SeoKeywords = f.SeoKeywords,
                        SeoTitle = f.SeoTitle,
                        ShortDescription = f.ShortDescription,
                        SocialDescription = f.SocialDescription,
                        SocialTitle = f.SocialTitle,
                        Title = f.Title,
                        Url = f.Url,
                        UserId = f.AddUserId,
                        HistoryNo = f.HistoryNo,
                        BookMarkStatus = requestedUserId.HasValue && f.NewsBookmarks.Any(f => f.UserId == requestedUserId),
                        UseTitle = f.UseTitle ?? false,
                        ReporterId = f.ReporterId
                    }).ToListAsync();
                if (list.Any())
                {
                    foreach (var item in list)
                    {
                        if (item != null)
                        {
                            if (item.ReporterId.HasValue)
                            {
                                item.Reporter = await Db.Reporters.Where(f => f.Id == item.ReporterId.Value && !f.Deleted)
                                    .Select(f => new ReporterDto
                                    {
                                        FullName = f.FullName,
                                        Id = f.Id,
                                        ProfileImage = f.ProfileImageId == null ? null : new FileDto { FileName = f.ProfileImage.FileName.GetFullFilePath(), Id = f.ProfileImage.Id },
                                        ProfileImageId = f.ProfileImageId,
                                        Url = f.Url
                                    }).FirstOrDefaultAsync();
                            }
                            var relatedNewsList = await Db.NewsRelatedNews.Where(f => f.NewsId == item.Id).Select(f => new MainPageRelatedNewsDto
                            {
                                RelatedNewsId = f.RelatedNewsId,
                                RelatedNews = new MainPageRelatedNewsDetailsDto
                                {
                                    Id = f.RelatedNewsId,
                                    Thumbnail = f.RelatedNews.NewsFiles.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
                                        f.RelatedNews.NewsFiles.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.FileName.GetFullFilePath() : "".GetDefaultImageUrl(),
                                    Title = f.RelatedNews.Title,
                                    Url = f.RelatedNews.Url,
                                    HistoryNo = f.RelatedNews.HistoryNo,
                                    NewsTypeEntityId = f.RelatedNews.NewsTypeEntityId
                                }
                            }).ToListAsync();

                            var newsIds = new List<int> { item.Id };
                            if (relatedNewsList.Any())
                            {
                                newsIds.AddRange(relatedNewsList.Select(h => h.RelatedNewsId).ToList());
                            }

                            var categories = Db.NewsCategories.AsNoTracking().Where(f => newsIds.Contains(f.NewsId))
                                        .Select(f => new { f.NewsId, f.CategoryId, CategoryUrl = f.Category.Url });

                            var positions = Db.NewsPositions.AsNoTracking().Where(f => newsIds.Contains(f.NewsId))
                                            .Select(f => new { f.NewsId, f.PositionEntityId });

                            if (relatedNewsList.Any())
                            {
                                relatedNewsList.ForEach(f =>
                                {
                                    f.RelatedNews.Url = GetUrl2(f.RelatedNews.NewsTypeEntityId, f.RelatedNews.Url,
                                f.RelatedNews.HistoryNo, categories.Where(r => r.NewsId == f.RelatedNewsId).Select(g => g.CategoryUrl).FirstOrDefault());
                                });
                            }


                            item.Url = GetUrl2(item.NewsTypeEntityId, item.Url,
                                item.HistoryNo, categories.Where(r => r.NewsId == item.Id).Select(g => g.CategoryUrl).FirstOrDefault());

                            var files = await Db.NewsFiles.Where(f => f.NewsId == item.Id).Select(f => new MainPageNewsFileDto
                            {
                                CoverFileName = f.VideoCoverFile == null ? "" : f.VideoCoverFile.FileName,
                                Description = f.Description,
                                FileName = f.File.FileName,
                                NewsFileTypeEntityId = f.NewsFileTypeEntityId,
                                Order = f.Order,
                                Title = f.Title
                            }).OrderBy(t => t.Order).ToListAsync();
                            if (files.Any())
                            {
                                files.ForEach(f =>
                                {
                                    f.CoverFileName = f.CoverFileName.GetFullFilePath();
                                    f.FileName = f.FileName.GetFullFilePath();
                                });
                            }
                            var tags = await Db.NewsTags.Where(f => f.NewsId == item.Id).Select(f => new NewsTagDto
                            {
                                TagId = f.TagId,
                                TagName = f.Tag.TagName,
                                Url = f.Tag.Url
                            }).ToListAsync();

                            item.NewsFileList = files;
                            item.NewsRelatedNewsList = relatedNewsList;
                            item.NewsTagList = tags;
                            var categryIds = categories.Where(g => g.NewsId == item.Id).Select(g => g.CategoryId).ToList();
                            item.NewsCategoryList = await Db.Categories.Where(f => categryIds.Contains(f.Id)).Select(f => new CategoryDto
                            {
                                CategoryName = f.CategoryName,
                                Id = f.Id,
                                StyleCode = f.StyleCode,
                                Url = f.Url
                            }).ToListAsync();
                        }
                    }
                }
                var total = query.Count();
                return new Tuple<List<NewsDetailPageDto>, int>(list, total);
            }
            return null;
        }

        public async Task<NewsDetailPageDto> GetNewsWithDetails(string url, int? id = null, bool preview = false, int? requestedUserId = null)
        {
            var query = preview ? Db.News.AsNoTracking().Where(f => !f.Deleted) : GetActiveList();
            NewsDetailPageDto item = null;
            if (id.HasValue)
            {
                query = query.Where(f => f.Id == id.Value);
            }
            else
            {
                var historyNo = url.GetHistoryNoFromUrl();
                query = query.Where(f => f.HistoryNo == historyNo);
            }
            if (query != null)
            {
                item = await query.Select(f => new NewsDetailPageDto
                {
                    HtmlContent = f.HtmlContent,
                    Id = f.Id,
                    InnerTitle = f.InnerTitle,
                    NewsAgencyEntityId = f.NewsAgencyEntityId,
                    NewsTypeEntityId = f.NewsTypeEntityId,
                    PublishDate = f.PublishDate,
                    PublishTime = f.PublishTime,
                    SeoDescription = f.SeoDescription,
                    SeoKeywords = f.SeoKeywords,
                    SeoTitle = f.SeoTitle,
                    ShortDescription = f.ShortDescription,
                    SocialDescription = f.SocialDescription,
                    SocialTitle = f.SocialTitle,
                    Title = f.Title,
                    Url = f.Url,
                    UserId = f.AddUserId,
                    HistoryNo = f.HistoryNo,
                    BookMarkStatus = requestedUserId.HasValue && f.NewsBookmarks.Any(f => f.UserId == requestedUserId),
                    UseTitle = f.UseTitle ?? false,
                    ReporterId = f.ReporterId
                }).FirstOrDefaultAsync();

                if (item != null)
                {
                    if (item.ReporterId.HasValue)
                    {
                        item.Reporter = await Db.Reporters.Where(f => f.Id == item.ReporterId.Value && !f.Deleted)
                            .Select(f => new ReporterDto
                            {
                                FullName = f.FullName,
                                Id = f.Id,
                                ProfileImage = f.ProfileImageId == null ? null : new FileDto { FileName = f.ProfileImage.FileName.GetFullFilePath(), Id = f.ProfileImage.Id },
                                ProfileImageId = f.ProfileImageId,
                                Url = f.Url
                            }).FirstOrDefaultAsync();
                    }
                    var relatedNewsList = await Db.NewsRelatedNews.Where(f => f.NewsId == item.Id).Select(f => new MainPageRelatedNewsDto
                    {
                        RelatedNewsId = f.RelatedNewsId,
                        RelatedNews = new MainPageRelatedNewsDetailsDto
                        {
                            Id = f.RelatedNewsId,
                            Thumbnail = f.RelatedNews.NewsFiles.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
                                f.RelatedNews.NewsFiles.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.FileName.GetFullFilePath() : "".GetDefaultImageUrl(),
                            Title = f.RelatedNews.Title,
                            Url = f.RelatedNews.Url,
                            HistoryNo = f.RelatedNews.HistoryNo,
                            NewsTypeEntityId = f.RelatedNews.NewsTypeEntityId,
                            UseTitle = f.RelatedNews.UseTitle ?? true
                        }
                    }).ToListAsync();

                    var newsIds = new List<int> { item.Id };
                    if (relatedNewsList.Any())
                    {
                        newsIds.AddRange(relatedNewsList.Select(h => h.RelatedNewsId));
                    }

                    var categories = Db.NewsCategories.AsNoTracking().Where(f => newsIds.Contains(f.NewsId))
                                .Select(f => new { f.NewsId, f.CategoryId, CategoryUrl = f.Category.Url });

                    var positions = Db.NewsPositions.AsNoTracking().Where(f => newsIds.Contains(f.NewsId))
                                    .Select(f => new { f.NewsId, f.PositionEntityId });

                    if (relatedNewsList.Any())
                    {
                        relatedNewsList.ForEach(f =>
                        {
                            f.RelatedNews.Url = GetUrl2(f.RelatedNews.NewsTypeEntityId, f.RelatedNews.Url,
                        f.RelatedNews.HistoryNo, categories.Where(r => r.NewsId == f.RelatedNewsId).Select(g => g.CategoryUrl).FirstOrDefault());
                        });
                    }

                    item.Url = GetUrl2(item.NewsTypeEntityId, item.Url,
                        item.HistoryNo, categories.Where(r => r.NewsId == item.Id).Select(g => g.CategoryUrl).FirstOrDefault());

                    var files = await Db.NewsFiles.Where(f => f.NewsId == item.Id).Select(f => new MainPageNewsFileDto
                    {
                        CoverFileName = f.VideoCoverFile == null ? "" : f.VideoCoverFile.FileName,
                        Description = f.Description,
                        FileName = f.File.FileName,
                        NewsFileTypeEntityId = f.NewsFileTypeEntityId,
                        Order = f.Order,
                        Title = f.Title
                    }).OrderBy(t => t.Order).ToListAsync();
                    if (files.Any())
                    {
                        files.ForEach(f =>
                        {
                            f.CoverFileName = f.CoverFileName.GetFullFilePath();
                            f.FileName = f.FileName.GetFullFilePath();
                        });
                    }
                    var tags = await Db.NewsTags.Where(f => f.NewsId == item.Id).Select(f => new NewsTagDto
                    {
                        TagId = f.TagId,
                        TagName = f.Tag.TagName,
                        Url = f.Tag.Url
                    }).ToListAsync();

                    item.NewsFileList = files;
                    item.NewsRelatedNewsList = relatedNewsList;
                    item.NewsTagList = tags;

                    var categryIds = categories.Where(g => g.NewsId == item.Id).Select(g => g.CategoryId).ToList();
                    item.NewsCategoryList = await Db.Categories.Where(f => categryIds.Contains(f.Id)).Select(f => new CategoryDto
                    {
                        CategoryName = f.CategoryName,
                        Id = f.Id,
                        StyleCode = f.StyleCode,
                        Url = f.Url
                    }).ToListAsync();
                }
            }

            return item;
        }

        public async Task<List<MostSharedNewsDto>> GetMostShareNewsList(int limit)
        {
            DateTime start = DateTime.Now.AddDays(-1 * AppSettingsExtension.GetValue<int>("MostShareNewsDays"));
            var languageId = CommonHelper.CurrentLanguageId;
            var query = await GetActiveList().Where(f => f.PublishDate >= start &&
            f.NewsCategories.Any(t => languageId == 0 || t.Category.LanguageId == languageId)).Select(f => new
            {
                Id = f.Id,
                ShortDescription = f.ShortDescription,
                Title = f.Title,
                Url = f.Url,
                HistoryNo = f.HistoryNo,
                NewsTypeEntityId = f.NewsTypeEntityId,
                AuthorNameSurename = f.Author.NameSurename,
                ShareCount = f.NewsCounters.Where(u => u.CounterEntityId == (int)NewsCounterEntities.TotalShareCount).Select(r => r.Value).FirstOrDefault() ?? 0,
                f.UseTitle
            }).OrderByDescending(f => f.ShareCount).ThenByDescending(f => f.Id).Take(limit.CheckLimit()).ToListAsync();

            var newsIds = query.Select(f => f.Id).Distinct();

            var files = await Db.NewsFiles.AsNoTracking().Where(f => newsIds.Contains(f.NewsId) && f.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage)
               .Select(f => new { f.NewsId, FileName = f.File.FileName.GetFullFilePath() }).ToListAsync();

            var categories = await Db.NewsCategories.AsNoTracking().Where(f => newsIds.Contains(f.NewsId))
                .Select(f => new { f.NewsId, CategoryUrl = f.Category.Url }).ToListAsync();

            var positions = await Db.NewsPositions.AsNoTracking().Where(f => newsIds.Contains(f.NewsId))
                .Select(f => new { f.NewsId, f.PositionEntityId }).ToListAsync();


            return query.Select(u => new MostSharedNewsDto
            {
                ImageUrl = files.Where(t => t.NewsId == u.Id).Select(g => g.FileName).FirstOrDefault() ?? "".GetDefaultImageUrl(),
                Url = GetUrl2(u.NewsTypeEntityId, u.Url, u.HistoryNo,
                 categories.Where(t => t.NewsId == u.Id).Select(g => g.CategoryUrl).FirstOrDefault()),
                Id = u.Id,
                ShortDescription = u.ShortDescription,
                Title = u.Title,
                ShareCount = u.ShareCount,
                UseTitle = u.UseTitle ?? false
            }).ToList();
        }

        public async Task<List<MostViewedNewsDto>> GetLastWeekMostViewedNews(int limit)
        {
            DateTime start = DateTime.Now.AddDays(-1 * AppSettingsExtension.GetValue<int>("LastWeekMostViewedNewsDays"));
            var languageId = CommonHelper.CurrentLanguageId;
            var query = await GetActiveList().Where(f => f.PublishDate >= start &&
            f.NewsCategories.Any(t => languageId == 0 || t.Category.LanguageId == languageId) &&
            !f.NewsProperties.Any(g => g.PropertyEntityId == (int)NewsPropertyEntities.NotShowInMostViewdNews && g.Value == true)
            ).Select(f => new
            {
                Id = f.Id,
                ShortDescription = f.ShortDescription,
                Title = f.Title,
                Url = f.Url,
                HistoryNo = f.HistoryNo,
                NewsTypeEntityId = f.NewsTypeEntityId,
                ViewCount = f.NewsHitDetails.Count(),
                f.UseTitle
            }).OrderByDescending(f => f.ViewCount).Take(limit.CheckLimit()).ToListAsync();

            var newsIds = query.Select(f => f.Id).Distinct();

            var files = await Db.NewsFiles.AsNoTracking().Where(f => newsIds.Contains(f.NewsId) && f.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage)
                .Select(f => new { f.NewsId, FileName = f.File.FileName.GetFullFilePath() }).ToListAsync();

            var categories = await Db.NewsCategories.AsNoTracking().Where(f => newsIds.Contains(f.NewsId))
                .Select(f => new { f.NewsId, CategoryUrl = f.Category.Url }).ToListAsync();

            var positions = await Db.NewsPositions.AsNoTracking().Where(f => newsIds.Contains(f.NewsId))
                .Select(f => new { f.NewsId, f.PositionEntityId }).ToListAsync();


            return query.Select(u => new MostViewedNewsDto
            {
                ImageUrl = files.Where(t => t.NewsId == u.Id).Select(g => g.FileName).FirstOrDefault() ?? "".GetDefaultImageUrl(),
                Url = GetUrl2(u.NewsTypeEntityId, u.Url, u.HistoryNo,
                categories.Where(t => t.NewsId == u.Id).Select(g => g.CategoryUrl).FirstOrDefault()),
                Id = u.Id,
                ShortDescription = u.ShortDescription,
                Title = u.Title,
                ViewCount = u.ViewCount,
                UseTitle = u.UseTitle ?? false
            }).ToList();
        }

        public async Task<News> GetView(Expression<Func<News, bool>> filter = null)
        {
            return await Db.News.Where(filter).Include(f => f.NewsTypeEntity)
                .Include(f => f.Reporter).ThenInclude(f => f.ProfileImage)
                .Include(f => f.NewsAgencyEntity)
                .Include(f => f.NewsCategories).ThenInclude(f => f.Category)
                .Include(f => f.NewsFiles).ThenInclude(f => f.File)
                .Include(f => f.NewsFiles).ThenInclude(f => f.VideoCoverFile)
                .Include(f => f.NewsTags).ThenInclude(f => f.Tag)
                .Include(f => f.Author).ThenInclude(f => f.PhotoFile)
                .Include(f => f.NewsRelatedNewsNews).ThenInclude(f => f.RelatedNews).ThenInclude(f => f.NewsFiles).ThenInclude(f => f.File)
                .Include(f => f.NewsPositions).Include(f => f.NewsProperties).FirstOrDefaultAsync();
        }

        public async Task<DashboardStatisticsDto> GetDashboardStatistics()
        {
            var yesterday = DateTime.Now.AddDays(-1 * AppSettingsExtension.GetValue<int>("DashboardStatisticsDays")).Date;
            var newsList = GetActiveList().Include(f => f.NewsComments).Include(f => f.NewsCounters);

            return new DashboardStatisticsDto
            {
                TodayCreatedCommentCount = await newsList.Where(f => f.PublishDate.Value.Date >= yesterday).Select(f => f.NewsComments.Count(g => !g.Deleted)).SumAsync(),
                TodayCreatedNewsCount = await newsList.CountAsync(f => f.PublishDate.Value.Date >= yesterday),
                TodayCreatedPhotoGalleryCount = await newsList.CountAsync(f => f.PublishDate.Value.Date >= yesterday && f.NewsTypeEntityId == (int)NewsTypeEntities.ImageGallery),
                TodayCreatedVideoGalleryCount = await newsList.CountAsync(f => f.PublishDate.Value.Date >= yesterday && f.NewsTypeEntityId == (int)NewsTypeEntities.VideoGallery),
                TodayNewsReadCounter = await newsList.Where(f => f.PublishDate.Value.Date >= yesterday && f.NewsCounters.Any(g => g.CounterEntityId == (int)NewsCounterEntities.TotalViewCount))
                .Select(f => f.NewsCounters.First(g => g.CounterEntityId == (int)NewsCounterEntities.TotalViewCount).Value).SumAsync() ?? 0,
                TotalCommentCount = await newsList.Select(f => f.NewsComments.Count(g => !g.Deleted)).SumAsync(),
                TotalNewsCount = await newsList.CountAsync(),
                TotalNewsReadCounter = await newsList.Where(f => f.NewsCounters.Any(g => g.CounterEntityId == (int)NewsCounterEntities.TotalViewCount))
                .Select(f => f.NewsCounters.First(g => g.CounterEntityId == (int)NewsCounterEntities.TotalViewCount).Value).SumAsync() ?? 0,
                TotalPhotoGalleryCount = await newsList.CountAsync(f => f.NewsTypeEntityId == (int)NewsTypeEntities.ImageGallery),
                TotalUserCount = await Db.Users.CountAsync(f => !f.Deleted),
                TotalVideoGalleryCount = await newsList.CountAsync(f => f.NewsTypeEntityId == (int)NewsTypeEntities.VideoGallery)
            };
        }

        public async Task AddNewsWithDetails(News news, List<NewsCategory> categories, List<NewsFile> files,
          List<NewsPosition> positions, List<NewsProperty> properties,
          List<NewsRelatedNews> newsRelatedNews, List<NewsTag> tags, bool isAdd)
        {
            using (var transaction = Db.Database.BeginTransaction())
            {
                try
                {
                    var newsHistory = await Db.News.Where(f => f.HistoryNo == news.HistoryNo && !f.Deleted).ToListAsync();
                    var lastNews = newsHistory.FirstOrDefault(f => f.IsLastNews);
                    newsHistory.ForEach(n =>
                    {
                        n.IsLastNews = false;
                    });
                    Db.SaveChanges();

                    Db.News.Add(news);
                    await Db.SaveChangesAsync();

                    var counterEntities = await Db.Entities.Where(f => f.EntityGroupId == (int)EntityGroupType.CounterEntities).Select(g => g.Id).ToListAsync();
                    counterEntities.ForEach(entityId =>
                    {
                        var value = lastNews == null ? 0 :
                        Db.NewsCounters.Where(f => f.NewsId == lastNews.Id && f.CounterEntityId == entityId).Select(f => f.Value).FirstOrDefault();

                        Db.NewsCounters.Add(new NewsCounter
                        {
                            CounterEntityId = entityId,
                            NewsId = news.Id,
                            Value = value
                        });
                    });


                    if (categories.HasValue())
                    {
                        categories.ForEach(c => c.NewsId = news.Id);
                        Db.NewsCategories.AddRange(categories);
                    }
                    if (files.HasValue())
                    {
                        files.ForEach(c => c.NewsId = news.Id);
                        Db.NewsFiles.AddRange(files);
                    }

                    if (positions.HasValue())
                    {
                        var lastPositions = isAdd || lastNews == null ? null : Db.NewsPositions.Where(f => f.NewsId == lastNews.Id && f.Order > 0).Select(f => new { f.Order, f.PositionEntityId }).ToList();
                        foreach (var pos in positions)
                        {
                            if (news.Active && !news.IsDraft && news.Approved.Value)
                            {
                                if (!lastPositions.HasValue())
                                {
                                    var activeNewsIds = GetActiveList().Select(u => u.Id);
                                    var list = Db.NewsPositions.Where(f => activeNewsIds.Contains(f.NewsId) && f.PositionEntityId == pos.PositionEntityId && f.Order > 0).Select(t => t.Id).ToList();
                                    if (list.HasValue())
                                    {
                                        foreach (var id in list)
                                        {
                                            var g = Db.NewsPositions.FirstOrDefault(r => r.Id == id);
                                            g.Order = g.Order + 1;
                                        }
                                        if (list.Count > 35)
                                        {
                                            var g = Db.NewsPositions.FirstOrDefault(r => r.Id == list.Last());
                                            g.Order = 0;
                                        }
                                    }
                                }
                                pos.Order = lastPositions == null ? 1 : lastPositions.Where(t => t.PositionEntityId == pos.PositionEntityId && t.Order > 0).Select(g => g.Order).DefaultIfEmpty(1).FirstOrDefault();
                            }
                            pos.NewsId = news.Id;
                            Db.NewsPositions.Add(pos);
                            var res = Db.SaveChanges();
                        }
                    }

                    if (properties.HasValue())
                    {
                        properties.ForEach(c => c.NewsId = news.Id);
                        Db.NewsProperties.AddRange(properties);
                    }

                    if (newsRelatedNews.HasValue())
                    {
                        newsRelatedNews.ForEach(c => c.NewsId = news.Id);
                        Db.NewsRelatedNews.AddRange(newsRelatedNews);
                    }

                    if (tags.HasValue())
                    {
                        tags.ForEach(c => c.NewsId = news.Id);
                        Db.NewsTags.AddRange(tags);
                    }

                    var newsIds = newsHistory.Select(f => f.Id).ToList();
                    var comments = Db.NewsComments.Where(f => !f.Deleted && newsIds.Contains(f.NewsId)).ToList();
                    if (comments.HasValue())
                    {
                        comments.ForEach(c => c.NewsId = news.Id);
                    }

                    var bookmarks = Db.NewsBookmarks.Where(f => newsIds.Contains(f.NewsId)).ToList();
                    if (bookmarks.HasValue())
                    {
                        bookmarks.ForEach(c => c.NewsId = news.Id);
                    }

                    if (lastNews != null)
                    {
                        Db.Database.ExecuteSqlRaw($"update public.news_hit_detail set news_id={news.Id} where news_id={lastNews.Id}");
                        Db.Database.ExecuteSqlRaw($"update public.news_hit set news_id={news.Id} where news_id={lastNews.Id}");
                    }

                    await Db.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public IQueryable<News> GetActiveList()
        {
            var now = DateTime.Now.AddMinutes(5);
            return Db.News.AsNoTracking().Where(f => !f.Deleted && f.Active && f.Approved.Value && !f.IsDraft && f.IsLastNews &&
            (f.PublishDate.Value.Date < now.Date ||
            (f.PublishDate.Value.Date == now.Date && f.PublishTime.Value <= now.TimeOfDay))
            ).AsQueryable();
        }

        public IQueryable<News> GetNewsListByCategoryUrl(string url, out int headingPositionEntityId)
        {
            var category = Db.Categories.AsNoTracking().Where(f => f.Url.ToLower() == url.ToLower() && !f.Deleted).Select(f => new { f.Id, f.HeadingPositionEntityId }).FirstOrDefault();
            if (category == null)
            {
                headingPositionEntityId = 0;
                return new List<News>().AsQueryable();
            }
            headingPositionEntityId = category.HeadingPositionEntityId ?? 0;
            return GetActiveList().Where(f => f.NewsCategories.Any(u => u.CategoryId == category.Id));
        }

        public IQueryable<News> GetNewsListByCategoryId(int id, out int headingPositionEntityId)
        {
            var category = Db.Categories.AsNoTracking().Where(f => f.Id == id && !f.Deleted).Select(f => new { f.Id, f.HeadingPositionEntityId }).FirstOrDefault();
            if (category == null)
            {
                headingPositionEntityId = 0;
                return new List<News>().AsQueryable();
            }
            headingPositionEntityId = category.HeadingPositionEntityId ?? 0;
            return GetActiveList().Where(f => f.NewsCategories.Any(u => u.CategoryId == category.Id));
        }

        public IQueryable<News> GetNewsListByTagUrl(string url)
        {
            var tagId = Db.Tags.AsNoTracking().Where(f => f.Url.ToLower() == url.ToLower() && !f.Deleted).Select(f => f.Id).FirstOrDefault();
            return GetActiveList().Where(f => f.NewsTags.Any(u => u.TagId == tagId));
        }

        public async Task<List<DashboardTopCommentNewsDto>> GetDashboardTopCommentNews(int limit)
        {
            var newsList = Db.News.AsNoTracking().Where(f => !f.Deleted && f.NewsComments.Any() && f.IsLastNews).Include(f => f.NewsComments).Include(f => f.NewsCounters);

            return await newsList.OrderByDescending(f => f.NewsComments.Count(g => !g.Deleted)).Select(news => new DashboardTopCommentNewsDto
            {
                CommentsCount = news.NewsComments.Count(g => !g.Deleted),
                NewsId = news.Id,
                PublishDate = news.PublishDate,
                ShortDescription = news.ShortDescription,
                Title = news.Title,
                Url = news.Url.GetUrl(news.HistoryNo, news.NewsTypeEntityId, news.NewsCategories.Select(e => e.Category.Url).FirstOrDefault()),
                TotalViewCount = news.NewsCounters.Any(g => g.CounterEntityId == (int)NewsCounterEntities.TotalViewCount) ?
                  (news.NewsCounters.First(g => g.CounterEntityId == (int)NewsCounterEntities.TotalViewCount).Value ?? 0) : 0

            }).Take(limit.CheckLimit()).ToListAsync();
        }

        public async Task<List<DashboardChartDataDto>> GetDashboardChartData(DateTime fromDate)
        {
            var categories = Db.NewsCategories.AsNoTracking()
                .Where(f => f.Category != null && !f.Category.Deleted && !f.News.Deleted && f.News.IsLastNews)
                .Include(f => f.Category)
                .Include(f => f.News).ThenInclude(f => f.NewsCounters);
            var res = await categories.ToListAsync();
            var grouped = res.GroupBy(g => g.Category.CategoryName);

            return grouped.Select(n => new DashboardChartDataDto
            {
                CategoryNamne = n.Key,
                TotalViewCount = grouped.Where(t => t.Key == n.Key).Sum(f => f.Where(t => t.News.PublishDate >= fromDate).Sum(h => h.News.NewsCounters.First(g => g.CounterEntityId == (int)NewsCounterEntities.TotalViewCount).Value)) ?? 0
            }).ToList();
        }

        public async Task IncreaseShareCount(int newsId)
        {
            var entityId = (int)NewsCounterEntities.TotalShareCount;
            var data = await Db.NewsCounters.FirstOrDefaultAsync(f => f.NewsId == newsId && f.CounterEntityId == entityId);
            if (data != null)
            {
                data.Value = !data.Value.HasValue ? 1 : data.Value + 1;
            }
            else
            {
                Db.NewsCounters.Add(new NewsCounter
                {
                    CounterEntityId = entityId,
                    NewsId = newsId,
                    Value = 1
                });
            }
            await Db.SaveChangesAsync();
        }
    }
}

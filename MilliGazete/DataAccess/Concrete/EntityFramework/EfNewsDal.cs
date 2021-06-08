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
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfNewsDal : EfEntityRepositoryBase<News>, INewsDal
    {
        public EfNewsDal(MilliGazeteDbContext milligazeteDb) : base(milligazeteDb)
        {
        }

        public async Task<News> GetView(Expression<Func<News, bool>> filter = null)
        {
            return await Db.News.Where(filter).Include(f => f.NewsTypeEntity)
                .Include(f => f.NewsAgencyEntity)
                .Include(f => f.NewsCategory).ThenInclude(f => f.Category)
                .Include(f => f.NewsFile).ThenInclude(f => f.File)
                .Include(f => f.NewsFile).ThenInclude(f => f.VideoCoverFile)
                .Include(f => f.NewsTag).ThenInclude(f => f.Tag)
                .Include(f => f.Author).ThenInclude(f => f.PhotoFile)
                .Include(f => f.NewsRelatedNewsNews).ThenInclude(f => f.RelatedNews).ThenInclude(f => f.NewsFile).ThenInclude(f => f.File)
                .Include(f => f.NewsPosition).Include(f => f.NewsProperty).FirstOrDefaultAsync();
        }

        public async Task<DashboardStatisticsDto> GetDashboardStatistics()
        {
            var yesterday = DateTime.Now.AddDays(-1).Date;
            var newsList = GetActiveList().Include(f => f.NewsComment).Include(f => f.NewsCounter);

            return new DashboardStatisticsDto
            {
                TodayCreatedCommentCount = await newsList.Where(f => f.PublishDate.Value.Date >= yesterday).Select(f => f.NewsComment.Count(g => !g.Deleted)).SumAsync(),
                TodayCreatedNewsCount = await newsList.CountAsync(f => f.PublishDate.Value.Date >= yesterday),
                TodayCreatedPhotoGalleryCount = await newsList.CountAsync(f => f.PublishDate.Value.Date >= yesterday && f.NewsTypeEntityId == (int)NewsTypeEntities.ImageGallery),
                TodayCreatedVideoGalleryCount = await newsList.CountAsync(f => f.PublishDate.Value.Date >= yesterday && f.NewsTypeEntityId == (int)NewsTypeEntities.VideoGallery),
                TodayNewsReadCounter = await newsList.Where(f => f.PublishDate.Value.Date >= yesterday && f.NewsCounter.Any(g => g.CounterEntityId == (int)NewsCounterEntities.TotalViewCount))
                .Select(f => f.NewsCounter.First(g => g.CounterEntityId == (int)NewsCounterEntities.TotalViewCount).Value).SumAsync() ?? 0,
                TotalCommentCount = await newsList.Select(f => f.NewsComment.Count(g => !g.Deleted)).SumAsync(),
                TotalNewsCount = await newsList.CountAsync(),
                TotalNewsReadCounter = await newsList.Where(f => f.NewsCounter.Any(g => g.CounterEntityId == (int)NewsCounterEntities.TotalViewCount))
                .Select(f => f.NewsCounter.First(g => g.CounterEntityId == (int)NewsCounterEntities.TotalViewCount).Value).SumAsync() ?? 0,
                TotalPhotoGalleryCount = await newsList.CountAsync(f => f.NewsTypeEntityId == (int)NewsTypeEntities.ImageGallery),
                TotalUserCount = await Db.User.CountAsync(f => !f.Deleted),
                TotalVideoGalleryCount = await newsList.CountAsync(f => f.NewsTypeEntityId == (int)NewsTypeEntities.VideoGallery)
            };
        }

        public async Task AddNewsWithDetails(News news, List<NewsCategory> categories, List<NewsFile> files,
            List<NewsPosition> positions, List<NewsProperty> properties,
            List<NewsRelatedNews> newsRelatedNews, List<NewsTag> tags)
        {
            using (var transaction = Db.Database.BeginTransaction())
            {
                try
                {
                    var newsHistory = Db.News.Where(f => f.HistoryNo == news.HistoryNo && !f.Deleted).ToList();
                    var lastNews = newsHistory.FirstOrDefault(f => f.IsLastNews);
                    newsHistory.ForEach(n =>
                    {
                        n.IsLastNews = false;
                    });
                    Db.SaveChanges();

                    Db.News.Add(news);
                    Db.SaveChanges();

                    var counterEntities = Db.Entity.Where(f => f.EntityGroupId == (int)EntityGroupType.CounterEntities).Select(g => g.Id).ToList();
                    counterEntities.ForEach(entityId =>
                    {
                        var value = lastNews == null ? 0 :
                        Db.NewsCounter.Where(f => f.NewsId == lastNews.Id && f.CounterEntityId == entityId).Select(f => f.Value).FirstOrDefault();

                        Db.NewsCounter.Add(new NewsCounter
                        {
                            CounterEntityId = entityId,
                            NewsId = news.Id,
                            Value = value
                        });
                    });


                    if (categories.HasValue())
                    {
                        categories.ForEach(c => c.NewsId = news.Id);
                        Db.NewsCategory.AddRange(categories);
                    }
                    if (files.HasValue())
                    {
                        files.ForEach(c => c.NewsId = news.Id);
                        Db.NewsFile.AddRange(files);
                    }

                    if (positions.HasValue())
                    {
                        positions.ForEach(c =>
                        {
                            c.Order = 1;
                            c.NewsId = news.Id;
                        });
                        Db.NewsPosition.AddRange(positions);
                    }



                    if (properties.HasValue())
                    {
                        properties.ForEach(c => c.NewsId = news.Id);
                        Db.NewsProperty.AddRange(properties);
                    }

                    if (newsRelatedNews.HasValue())
                    {
                        newsRelatedNews.ForEach(c => c.NewsId = news.Id);
                        Db.NewsRelatedNews.AddRange(newsRelatedNews);
                    }

                    if (tags.HasValue())
                    {
                        tags.ForEach(c => c.NewsId = news.Id);
                        Db.NewsTag.AddRange(tags);
                    }

                    var newsIds = newsHistory.Select(f => f.Id).ToList();
                    var comments = Db.NewsComment.Where(f => !f.Deleted && newsIds.Contains(f.NewsId)).ToList();
                    if (comments.HasValue())
                    {
                        comments.ForEach(c => c.NewsId = news.Id);
                    }

                    var bookmarks = Db.NewsBookmark.Where(f => newsIds.Contains(f.NewsId)).ToList();
                    if (bookmarks.HasValue())
                    {
                        bookmarks.ForEach(c => c.NewsId = news.Id);
                    }

                    Db.SaveChanges();

                    await transaction.CommitAsync();
                }
                catch (Exception ec)
                {
                    await transaction.RollbackAsync();
                    throw ec;
                }
            }
        }

        public IQueryable<News> GetActiveList()
        {
            return Db.News.AsNoTracking().Where(f => !f.Deleted && f.Active && f.Approved.Value && !f.IsDraft && f.IsLastNews).AsQueryable();
        }

        public IQueryable<News> GetNewsListByCategoryUrl(string url, out int headingPositionEntityId)
        {
            var category = Db.Category.AsNoTracking().Where(f => f.Url.ToLower() == url.ToLower() && !f.Deleted).Select(f => new { f.Id, f.HeadingPositionEntityId }).FirstOrDefault();
            if (category == null)
            {
                headingPositionEntityId = 0;
                return new List<News>().AsQueryable();
            }
            headingPositionEntityId = category.HeadingPositionEntityId ?? 0;
            return GetActiveList().Where(f => f.NewsCategory.Any(u => u.CategoryId == category.Id));
        }

        public IQueryable<News> GetNewsListByCategoryId(int id, out int headingPositionEntityId)
        {
            var category = Db.Category.AsNoTracking().Where(f => f.Id == id && !f.Deleted).Select(f => new { f.Id, f.HeadingPositionEntityId }).FirstOrDefault();
            if (category == null)
            {
                headingPositionEntityId = 0;
                return new List<News>().AsQueryable();
            }
            headingPositionEntityId = category.HeadingPositionEntityId ?? 0;
            return GetActiveList().Where(f => f.NewsCategory.Any(u => u.CategoryId == category.Id));
        }

        public IQueryable<News> GetNewsListByTagUrl(string url)
        {
            var tagId = Db.Tag.AsNoTracking().Where(f => f.Url.ToLower() == url.ToLower() && !f.Deleted).Select(f => f.Id).FirstOrDefault();
            return GetActiveList().Where(f => f.NewsTag.Any(u => u.TagId == tagId));
        }

        public async Task<List<DashboardTopCommentNewsDto>> GetDashboardTopCommentNews(int limit)
        {
            var newsList = Db.News.AsNoTracking().Where(f => !f.Deleted && f.NewsComment.Any() && f.IsLastNews).Include(f => f.NewsComment).Include(f => f.NewsCounter);

            return await newsList.OrderByDescending(f => f.NewsComment.Count(g => !g.Deleted)).Select(news => new DashboardTopCommentNewsDto
            {
                CommentsCount = news.NewsComment.Count(g => !g.Deleted),
                NewsId = news.Id,
                PublishDate = news.PublishDate,
                ShortDescription = news.ShortDescription,
                Title = news.Title,
                Url = news.GetUrl(),
                TotalViewCount = news.NewsCounter.Any(g => g.CounterEntityId == (int)NewsCounterEntities.TotalViewCount) ?
                  (news.NewsCounter.First(g => g.CounterEntityId == (int)NewsCounterEntities.TotalViewCount).Value ?? 0) : 0

            }).Take(limit.CheckLimit()).ToListAsync();
        }

        public async Task<List<DashboardChartDataDto>> GetDashboardChartData(DateTime fromDate)
        {
            var categories = Db.NewsCategory.AsNoTracking()
                .Where(f => f.Category != null && !f.Category.Deleted && !f.News.Deleted && f.News.IsLastNews)
                .Include(f => f.Category)
                .Include(f => f.News).ThenInclude(f => f.NewsCounter);
            var res = await categories.ToListAsync();
            var grouped = res.GroupBy(g => g.Category.CategoryName);

            return grouped.Select(n => new DashboardChartDataDto
            {
                CategoryNamne = n.Key,
                TotalViewCount = grouped.Where(t => t.Key == n.Key).Sum(f => f.Where(t => t.News.PublishDate >= fromDate).Sum(h => h.News.NewsCounter.First(g => g.CounterEntityId == (int)NewsCounterEntities.TotalViewCount).Value)) ?? 0
            }).ToList();
        }

        public async Task IncreaseShareCount(int newsId)
        {
            var entityId = (int)NewsCounterEntities.TotalShareCount;
            var data = await Db.NewsCounter.FirstOrDefaultAsync(f => f.NewsId == newsId && f.CounterEntityId == entityId);
            if (data != null)
            {
                data.Value = !data.Value.HasValue ? 1 : data.Value + 1;
            }
            else
            {
                Db.NewsCounter.Add(new NewsCounter
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

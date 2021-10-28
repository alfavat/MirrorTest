using AutoMapper;
using Entity.Dtos;
using Entity.Enums;
using Entity.Models;
using System;
using System.Linq;

namespace Business.AutoMapper
{
    public class NewsMapper : Profile
    {
        public NewsMapper()
        {

            CreateMap<NewsAddDto, News>()
                  .BeforeMap((dto, entity) => { entity.CreatedAt = DateTime.Now; entity.Approved = true; })
                  .ForMember(f => f.ShortDescription, g => g.MapFrom(u => u.ShortDescription.ParseHtml()))
                  .ForMember(f => f.Url, g => g.MapFrom(u => u.Url.ReplaceSpecialCharacters()))
                  .ForMember(f => f.PublishDate, g => g.MapFrom(t => t.PublishDate))
                  .ForMember(f => f.NewsCategories, g => g.Ignore())
                  .ForMember(f => f.NewsFiles, g => g.Ignore())
                  .ForMember(f => f.NewsPositions, g => g.Ignore())
                  .ForMember(f => f.NewsProperties, g => g.Ignore())
                  .ForMember(f => f.NewsRelatedNewsNews, g => g.Ignore())
                  .ForMember(f => f.NewsRelatedNewsRelatedNews, g => g.Ignore())
                  .ForMember(f => f.NewsTags, g => g.Ignore())
                  .ForMember(f => f.AddUserId, g =>
                  {
                      g.PreCondition(src => (src.NewsId <= 0));
                      g.MapFrom(src => src.UserId);
                  })
                  .ForMember(f => f.UpdateUserId, g =>
                  {
                      g.PreCondition(src => (src.NewsId > 0));
                      g.MapFrom(src => src.UserId);
                  });

            CreateMap<News, NewsViewDto>()
              .ForMember(f => f.Url, g => g.MapFrom(t => t.Url.GetUrl(t.HistoryNo, t.NewsTypeEntityId, t.NewsCategories.Select(e => e.Category.Url).FirstOrDefault())))
              .ForMember(f => f.Author, u => u.MapFrom(g => g.Author))
              .ForMember(f => f.Reporter, u => u.MapFrom(g => g.Reporter))
              .ForMember(f => f.AddUser, u => u.MapFrom(g => g.AddUser == null ? "" : g.AddUser.UserName))
              .ForMember(f => f.UpdateUser, u => u.MapFrom(g => g.UpdateUser == null ? "" : g.UpdateUser.UserName))
              .ForMember(f => f.NewsAgencyEntity, u => u.MapFrom(g => g.NewsAgencyEntity == null ? "" : Translator.GetByKey(g.NewsAgencyEntity.EntityName)))
              .ForMember(f => f.NewsTypeEntity, u => u.MapFrom(g => g.NewsTypeEntity == null ? "" : Translator.GetByKey(g.NewsTypeEntity.EntityName)))
              .ForMember(f => f.Thumbnail, u => u.MapFrom(g => g.NewsFiles != null && g.NewsFiles.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
              g.NewsFiles.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.FileName.GetFullFilePath() : "".GetDefaultImageUrl()))
              .ForMember(f => f.NewsRelatedNewsList, g => g.MapFrom(t => t.NewsRelatedNewsNews))
              .ForMember(f => f.NewsCategoryList, g => g.MapFrom(u => u.NewsCategories))
              .ForMember(f => f.NewsFileList, g => g.MapFrom(u => u.NewsFiles))
              .ForMember(f => f.NewsPositionList, g => g.MapFrom(u => u.NewsPositions))
              .ForMember(f => f.NewsPropertyList, g => g.MapFrom(u => u.NewsProperties))
             .ForMember(f => f.NewsTagList, g => g.MapFrom(u => u.NewsTags.Where(f => !f.Tag.Deleted)))
             .ForMember(f => f.NewsHitCount, g => g.MapFrom(u => u.NewsHitDetails != null ? u.NewsHitDetails.Count(t => t.CreatedAt >= DateTime.Now.AddMinutes(-2)) : 0));

            CreateMap<NewsViewDto, News>();
            CreateMap<News, NewsHistoryDto>()
                .ForMember(f => f.NewsId, g => g.MapFrom(t => t.Id))
                .ForMember(f => f.UserName, g => g.MapFrom(t => t.AddUser == null ? "" : t.AddUser.UserName))
                .ReverseMap();

            CreateMap<News, NewsDetailPageDto>()
              .ForMember(f => f.AddUser, u => u.MapFrom(g => g.AddUser == null ? "" : g.AddUser.UserName))
              .ForMember(f => f.Url, u => u.MapFrom(g => g.Url.GetUrl(g.HistoryNo, g.NewsTypeEntityId, g.NewsCategories.Select(e => e.Category.Url).FirstOrDefault())))
              .ForMember(f => f.NewsAgencyEntity, u => u.MapFrom(g => g.NewsAgencyEntity == null ? "" : Translator.GetByKey(g.NewsAgencyEntity.EntityName)))
              .ForMember(f => f.Reporter, u => u.MapFrom(g => g.Reporter == null ? null : g.Reporter))
              .ForMember(f => f.NewsTypeEntity, u => u.MapFrom(g => g.NewsTypeEntity == null ? "" : Translator.GetByKey(g.NewsTypeEntity.EntityName)))
              .ForMember(f => f.NewsRelatedNewsList, g => g.MapFrom(t => t.NewsRelatedNewsNews))
              .ForMember(f => f.NewsFileList, g => g.MapFrom(u => u.NewsFiles))
              .ForMember(f => f.NewsPropertyList, g => g.MapFrom(u => u.NewsProperties))
              .ForMember(f => f.NewsTagList, g => g.MapFrom(u => u.NewsTags.Where(f => !f.Tag.Deleted)))
              .ForMember(f => f.NewsCategoryList, g => g.MapFrom(u => u.NewsCategories.Select(x => x.Category)));

            CreateMap<News, FlashNewsDto>()
                .ForMember(f => f.AuthorNameSurename, g => g.MapFrom(u => u.Author == null ? "" : u.Author.NameSurename))
                .ForMember(f => f.NewsId, u => u.MapFrom(g => g.Id))
                .ForMember(f => f.Url, u => u.MapFrom(g => g.Url.GetUrl(g.HistoryNo, g.NewsTypeEntityId, g.NewsCategories.Select(e => e.Category.Url).FirstOrDefault())));

            CreateMap<News, BreakingNewsDto>()
                .ForMember(f => f.AuthorNameSurename, g => g.MapFrom(u => u.Author == null ? "" : u.Author.NameSurename))
                .ForMember(f => f.NewsId, u => u.MapFrom(g => g.Id))
                .ForMember(f => f.Url, u => u.MapFrom(g => g.Url.GetUrl(g.HistoryNo, g.NewsTypeEntityId, g.NewsCategories.Select(e => e.Category.Url).FirstOrDefault())));

            CreateMap<News, WideHeadingNewsDto>()
                .ForMember(f => f.AuthorNameSurename, g => g.MapFrom(u => u.Author == null ? "" : u.Author.NameSurename))
                .ForMember(f => f.NewsId, u => u.MapFrom(g => g.Id))
                .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFiles != null &&
                                g.NewsFiles.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.MainHeadingImage) ?
                                g.NewsFiles.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.MainHeadingImage).File.FileName.GetFullFilePath() : "".GetDefaultImageUrl()))
              .ForMember(f => f.Url, u => u.MapFrom(g => g.Url.GetUrl(g.HistoryNo, g.NewsTypeEntityId, g.NewsCategories.Select(e => e.Category.Url).FirstOrDefault())))
              .ForMember(f => f.NewsAgencyEntity, u => u.MapFrom(g => g.NewsAgencyEntity == null ? "" : Translator.GetByKey(g.NewsAgencyEntity.EntityName)))
              .ForMember(f => f.NewsTypeEntity, u => u.MapFrom(g => g.NewsTypeEntity == null ? "" : Translator.GetByKey(g.NewsTypeEntity.EntityName)))
              .ForMember(f => f.NewsRelatedNewsList, g => g.MapFrom(t => t.NewsRelatedNewsNews))
              .ForMember(f => f.NewsFileList, g => g.MapFrom(u => u.NewsFiles))
              .ForMember(f => f.NewsPropertyList, g => g.MapFrom(u => u.NewsProperties))
              .ForMember(f => f.NewsTagList, g => g.MapFrom(u => u.NewsTags.Where(f => !f.Tag.Deleted)))
              .ForMember(f => f.NewsCategoryList, g => g.MapFrom(u => u.NewsCategories.Select(x => x.Category)));

            CreateMap<News, LiveNarrotationNewsDto>()
                .ForMember(f => f.AuthorNameSurename, g => g.MapFrom(u => u.Author == null ? "" : u.Author.NameSurename))
                .ForMember(f => f.NewsId, u => u.MapFrom(g => g.Id))
                .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFiles != null &&
                                g.NewsFiles.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
                                g.NewsFiles.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.FileName.GetFullFilePath() : "".GetDefaultImageUrl()))
              .ForMember(f => f.Url, u => u.MapFrom(g => g.Url.GetUrl(g.HistoryNo, g.NewsTypeEntityId, g.NewsCategories.Select(e => e.Category.Url).FirstOrDefault())))
              .ForMember(f => f.NewsAgencyEntity, u => u.MapFrom(g => g.NewsAgencyEntity == null ? "" : Translator.GetByKey(g.NewsAgencyEntity.EntityName)))
              .ForMember(f => f.NewsTypeEntity, u => u.MapFrom(g => g.NewsTypeEntity == null ? "" : Translator.GetByKey(g.NewsTypeEntity.EntityName)))
              .ForMember(f => f.NewsRelatedNewsList, g => g.MapFrom(t => t.NewsRelatedNewsNews))
              .ForMember(f => f.NewsFileList, g => g.MapFrom(u => u.NewsFiles))
              .ForMember(f => f.NewsPropertyList, g => g.MapFrom(u => u.NewsProperties))
              .ForMember(f => f.NewsTagList, g => g.MapFrom(u => u.NewsTags.Where(f => !f.Tag.Deleted)))
              .ForMember(f => f.NewsCategoryList, g => g.MapFrom(u => u.NewsCategories.Select(x => x.Category)));

            CreateMap<News, SubHeadingDto>()
                .ForMember(f => f.AuthorNameSurename, g => g.MapFrom(u => u.Author == null ? "" : u.Author.NameSurename))
                .ForMember(f => f.NewsId, u => u.MapFrom(g => g.Id))
                .ForMember(f => f.Url, u => u.MapFrom(g => g.Url.GetUrl(g.HistoryNo, g.NewsTypeEntityId, g.NewsCategories.Select(e => e.Category.Url).FirstOrDefault())))
                .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFiles != null &&
                                g.NewsFiles.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.MainHeadingImage) ?
                                g.NewsFiles.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.MainHeadingImage).File.FileName.GetFullFilePath() : "".GetDefaultImageUrl()));

            CreateMap<News, MainHeadingDto>()
                .ForMember(f => f.AuthorNameSurename, g => g.MapFrom(u => u.Author == null ? "" : u.Author.NameSurename))
                .ForMember(f => f.NewsId, g => g.MapFrom(u => u.Id))
                .ForMember(f => f.Url, u => u.MapFrom(g => g.Url.GetUrl(g.HistoryNo, g.NewsTypeEntityId, g.NewsCategories.Select(e => e.Category.Url).FirstOrDefault())))
                .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFiles != null &&
                                g.NewsFiles.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.MainHeadingImage) ?
                                g.NewsFiles.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.MainHeadingImage).File.FileName.GetFullFilePath() : "".GetDefaultImageUrl()));

            CreateMap<News, MostViewedNewsDto>()
                .ForMember(f => f.Url, u => u.MapFrom(g => g.Url.GetUrl(g.HistoryNo, g.NewsTypeEntityId, g.NewsCategories.Select(e => e.Category.Url).FirstOrDefault())))
                .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFiles != null &&
                                g.NewsFiles.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
                                g.NewsFiles.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.FileName.GetFullFilePath() : "".GetDefaultImageUrl()))
                .ForMember(f => f.ViewCount, g => g.MapFrom(r => r.NewsCounters.Any(u => u.CounterEntityId == (int)NewsCounterEntities.TotalViewCount) ?
                r.NewsCounters.First(h => h.CounterEntityId == (int)NewsCounterEntities.TotalViewCount).Value : 0));

            CreateMap<News, MostHitNewsDto>()
                .ForMember(f => f.Url, u => u.MapFrom(g => g.Url.GetUrl(g.HistoryNo, g.NewsTypeEntityId, g.NewsCategories.Select(e => e.Category.Url).FirstOrDefault())))
                .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFiles != null &&
                                g.NewsFiles.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
                                g.NewsFiles.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.FileName.GetFullFilePath() : "".GetDefaultImageUrl()));

            CreateMap<News, MostSharedNewsDto>()
               .ForMember(f => f.AuthorNameSurename, g => g.MapFrom(u => u.Author == null ? "" : u.Author.NameSurename))
               .ForMember(f => f.Url, u => u.MapFrom(g => g.Url.GetUrl(g.HistoryNo, g.NewsTypeEntityId, g.NewsCategories.Select(e => e.Category.Url).FirstOrDefault())))
               .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFiles != null &&
                               g.NewsFiles.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
                               g.NewsFiles.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.FileName.GetFullFilePath() : "".GetDefaultImageUrl()))
               .ForMember(f => f.ShareCount, g => g.MapFrom(r => r.NewsCounters.Any(u => u.CounterEntityId == (int)NewsCounterEntities.TotalShareCount) ?
               r.NewsCounters.First(h => h.CounterEntityId == (int)NewsCounterEntities.TotalShareCount).Value : 0));

            CreateMap<News, MainPageNewsDto>()
                .ForMember(f => f.NewsId, u => u.MapFrom(g => g.Id))
                .ForMember(f => f.StyleCode, u => u.MapFrom(g => g.NewsCategories != null && g.NewsCategories.Any() ? g.NewsCategories.First().Category.StyleCode : ""))
                .ForMember(f => f.CategoryName, u => u.MapFrom(g => g.NewsCategories != null && g.NewsCategories.Any() ? g.NewsCategories.First().Category.CategoryName : ""))
                .ForMember(f => f.Url, u => u.MapFrom(g => g.Url.GetUrl(g.HistoryNo, g.NewsTypeEntityId, g.NewsCategories.Select(e => e.Category.Url).FirstOrDefault())))
                .ForMember(f => f.FileList, g => g.MapFrom(u => u.NewsFiles));

            CreateMap<News, MainPageVideoNewsDto>()
                .ForMember(f => f.AuthorNameSurename, g => g.MapFrom(u => u.Author == null ? "" : u.Author.NameSurename))
                .ForMember(f => f.NewsId, u => u.MapFrom(g => g.Id))
                .ForMember(f => f.Url, u => u.MapFrom(g => g.Url.GetUrl(g.HistoryNo, g.NewsTypeEntityId, g.NewsCategories.Select(e => e.Category.Url).FirstOrDefault())))
                .ForMember(f => f.VideoUrl, u => u.MapFrom(g => g.NewsFiles != null &&
                            g.NewsFiles.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.GalleryVideo) ?
                            g.NewsFiles.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.GalleryVideo).File.FileName.GetFullFilePath() : ""))
                .ForMember(f => f.VideoCoverImageUrl, u => u.MapFrom(g => g.NewsFiles != null &&
                            g.NewsFiles.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.GalleryVideo) ?
                            (
                            g.NewsFiles.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.GalleryVideo).VideoCoverFile == null ? "" :
                            g.NewsFiles.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.GalleryVideo).VideoCoverFile.FileName.GetFullFilePath()
                            ) : ""));

            CreateMap<News, MainPageImageNewsDto>()
                .ForMember(f => f.NewsId, u => u.MapFrom(g => g.Id))
                .ForMember(f => f.Url, u => u.MapFrom(g => g.Url.GetUrl(g.HistoryNo, g.NewsTypeEntityId, g.NewsCategories.Select(e => e.Category.Url).FirstOrDefault())))
                .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFiles != null &&
                            g.NewsFiles.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.GalleryPhoto) ?
                            g.NewsFiles.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.GalleryPhoto).File.FileName.GetFullFilePath() : "".GetDefaultImageUrl()));


            CreateMap<News, MainPageCategoryNewsDto>()
                .ForMember(f => f.AuthorNameSurename, g => g.MapFrom(u => u.Author == null ? "" : u.Author.NameSurename))
                .ForMember(f => f.NewsId, g => g.MapFrom(u => u.Id))
                .ForMember(f => f.Url, u => u.MapFrom(g => g.Url.GetUrl(g.HistoryNo, g.NewsTypeEntityId, g.NewsCategories.Select(e => e.Category.Url).FirstOrDefault())))
                .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFiles != null &&
                            g.NewsFiles.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
                            g.NewsFiles.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.FileName.GetFullFilePath() : "".GetDefaultImageUrl()));

            CreateMap<News, MainPageTagNewsDto>()
               .ForMember(f => f.AuthorNameSurename, g => g.MapFrom(u => u.Author == null ? "" : u.Author.NameSurename))
               .ForMember(f => f.NewsId, g => g.MapFrom(u => u.Id))
               .ForMember(f => f.Url, u => u.MapFrom(g => g.Url.GetUrl(g.HistoryNo, g.NewsTypeEntityId, g.NewsCategories.Select(e => e.Category.Url).FirstOrDefault())))
               .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFiles != null &&
                           g.NewsFiles.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
                           g.NewsFiles.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.FileName.GetFullFilePath() : "".GetDefaultImageUrl()));

            CreateMap<News, MainPageSearchNewsDto>()
               .ForMember(f => f.AuthorNameSurename, g => g.MapFrom(u => u.Author == null ? "" : u.Author.NameSurename))
               .ForMember(f => f.NewsId, g => g.MapFrom(u => u.Id))
               .ForMember(f => f.Url, u => u.MapFrom(g => g.Url.GetUrl(g.HistoryNo, g.NewsTypeEntityId, g.NewsCategories.Select(e => e.Category.Url).FirstOrDefault())))
               .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFiles != null &&
                           g.NewsFiles.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
                           g.NewsFiles.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.FileName.GetFullFilePath() : "".GetDefaultImageUrl()));

            CreateMap<News, NewsItem>()
              .ForMember(f => f.Url, u => u.MapFrom(g => g.Url.GetUrl(g.HistoryNo, g.NewsTypeEntityId, g.NewsCategories.Select(e => e.Category.Url).FirstOrDefault())))
              .ForMember(f => f.AgencyName, u => u.MapFrom(t => t.NewsAgencyEntity == null ? "MilliGazete" : Translator.GetByKey(t.NewsAgencyEntity.EntityName)))
              .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFiles != null &&
                              g.NewsFiles.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
                              g.NewsFiles.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.FileName.GetFullFilePath() : "".GetDefaultImageUrl()))
              .ForMember(f => f.Keywords, u => u.MapFrom(t => t.SeoKeywords))
              .ForMember(f => f.LastMod, u => u.MapFrom(t => t.CreatedAt))
              .ForMember(f => f.PulishDate, u => u.MapFrom(t => t.PublishDate))
              .ForMember(f => f.CategoryUrl, u => u.MapFrom(t => t.NewsCategories.Any(y => !y.Category.Deleted) ? t.NewsCategories.First(y => !y.Category.Deleted).Category.Url : ""));

            CreateMap<News, MainPageFourHillNewsDto>()
              .ForMember(f => f.AuthorNameSurename, g => g.MapFrom(u => u.Author == null ? "" : u.Author.NameSurename))
              .ForMember(f => f.NewsId, u => u.MapFrom(g => g.Id))
              .ForMember(f => f.Url, u => u.MapFrom(g => g.Url.GetUrl(g.HistoryNo, g.NewsTypeEntityId, g.NewsCategories.Select(e => e.Category.Url).FirstOrDefault())))
              .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFiles != null &&
                              g.NewsFiles.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
                              g.NewsFiles.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.FileName.GetFullFilePath() : "".GetDefaultImageUrl()));

            CreateMap<News, MainPageHeadlineSideNewsDto>()
             .ForMember(f => f.NewsId, u => u.MapFrom(g => g.Id))
             .ForMember(f => f.Url, u => u.MapFrom(g => g.Url.GetUrl(g.HistoryNo, g.NewsTypeEntityId, g.NewsCategories.Select(e => e.Category.Url).FirstOrDefault())))
             .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFiles != null &&
                             g.NewsFiles.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
                             g.NewsFiles.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.FileName.GetFullFilePath() : "".GetDefaultImageUrl()));

            CreateMap<News, MainPageArticleDto>()
               .ForMember(f => f.ArticleId, u => u.MapFrom(g => g.Id))
               .ForMember(f => f.Author, u => u.MapFrom(g => g.Author))
               .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFiles != null &&
                           g.NewsFiles.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
                           g.NewsFiles.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.FileName.GetFullFilePath() : "".GetDefaultImageUrl()));

            CreateMap<News, ArticleDto>()
                .ForMember(f => f.Url, g => g.MapFrom(t => t.Url.GetUrl(t.HistoryNo, t.NewsTypeEntityId, t.NewsCategories.Select(e => e.Category.Url).FirstOrDefault())))
              .ForMember(f => f.Author, u => u.MapFrom(g => g.Author))
              .ForMember(f => f.ReadCount, g => g.MapFrom(r => r.NewsCounters.Any(u => u.CounterEntityId == (int)NewsCounterEntities.TotalViewCount) ?
                r.NewsCounters.First(h => h.CounterEntityId == (int)NewsCounterEntities.TotalViewCount).Value : 0))
              .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFiles != null &&
                          g.NewsFiles.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
                          g.NewsFiles.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.FileName.GetFullFilePath() : "".GetDefaultImageUrl()));


            CreateMap<News, NewsPagingViewDto>()
          .ForMember(f => f.Url, g => g.MapFrom(t => t.Url.GetUrl(t.HistoryNo, t.NewsTypeEntityId, t.NewsCategories.Select(e => e.Category.Url).FirstOrDefault())))
          .ForMember(f => f.AddUser, u => u.MapFrom(g => g.AddUser == null ? "" : g.AddUser.UserName))
          .ForMember(f => f.NewsAgencyEntity, u => u.MapFrom(g => g.NewsAgencyEntity == null ? "" : Translator.GetByKey(g.NewsAgencyEntity.EntityName)))
          .ForMember(f => f.NewsTypeEntity, u => u.MapFrom(g => g.NewsTypeEntity == null ? "" : Translator.GetByKey(g.NewsTypeEntity.EntityName)))
          .ForMember(f => f.Thumbnail, u => u.MapFrom(g => g.NewsFiles.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
          g.NewsFiles.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.FileName.GetFullFilePath() : "".GetDefaultImageUrl()))
         .ForMember(f => f.NewsHitCount, g => g.MapFrom(u => u.NewsHitDetails.Any() ? u.NewsHitDetails.Count(t => t.CreatedAt >= DateTime.Now.AddMinutes(-2)) : 0));

        }
    }
}

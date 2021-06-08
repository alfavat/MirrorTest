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
                  .ForMember(f => f.NewsCategory, g => g.Ignore())
                  .ForMember(f => f.NewsFile, g => g.Ignore())
                  .ForMember(f => f.NewsPosition, g => g.Ignore())
                  .ForMember(f => f.NewsProperty, g => g.Ignore())
                  .ForMember(f => f.NewsRelatedNewsNews, g => g.Ignore())
                  .ForMember(f => f.NewsRelatedNewsRelatedNews, g => g.Ignore())
                  .ForMember(f => f.NewsTag, g => g.Ignore())
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
              .ForMember(f => f.Url, g => g.MapFrom(t => t.GetUrl()))
              .ForMember(f => f.Author, u => u.MapFrom(g => g.Author))
              .ForMember(f => f.AddUser, u => u.MapFrom(g => g.AddUser == null ? "" : g.AddUser.UserName))
              .ForMember(f => f.UpdateUser, u => u.MapFrom(g => g.UpdateUser == null ? "" : g.UpdateUser.UserName))
              .ForMember(f => f.NewsAgencyEntity, u => u.MapFrom(g => g.NewsAgencyEntity == null ? "" : Translator.GetByKey(g.NewsAgencyEntity.EntityName)))
              .ForMember(f => f.NewsTypeEntity, u => u.MapFrom(g => g.NewsTypeEntity == null ? "" : Translator.GetByKey(g.NewsTypeEntity.EntityName)))
              .ForMember(f => f.Thumbnail, u => u.MapFrom(g => g.NewsFile != null && g.NewsFile.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
              g.NewsFile.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.GetFullFilePath() : null))
              .ForMember(f => f.NewsRelatedNewsList, g => g.MapFrom(t => t.NewsRelatedNewsNews))
              .ForMember(f => f.NewsCategoryList, g => g.MapFrom(u => u.NewsCategory))
              .ForMember(f => f.NewsFileList, g => g.MapFrom(u => u.NewsFile))
              .ForMember(f => f.NewsPositionList, g => g.MapFrom(u => u.NewsPosition))
              .ForMember(f => f.NewsPropertyList, g => g.MapFrom(u => u.NewsProperty))
             .ForMember(f => f.NewsTagList, g => g.MapFrom(u => u.NewsTag.Where(f => !f.Tag.Deleted)))
             .ForMember(f => f.NewsHitCount, g => g.MapFrom(u => u.NewsHitDetail != null ? u.NewsHitDetail.Count(t => t.CreatedAt >= DateTime.Now.AddMinutes(-2)) : 0));

            CreateMap<NewsViewDto, News>();
            CreateMap<News, NewsHistoryDto>()
                .ForMember(f => f.NewsId, g => g.MapFrom(t => t.Id))
                .ForMember(f => f.UserName, g => g.MapFrom(t => t.AddUser == null ? "" : t.AddUser.UserName))
                .ReverseMap();

            CreateMap<News, NewsDetailPageDto>()
              .ForMember(f => f.AddUser, u => u.MapFrom(g => g.AddUser == null ? "" : g.AddUser.UserName))
              .ForMember(f => f.Url, u => u.MapFrom(g => g.GetUrl()))
              .ForMember(f => f.NewsAgencyEntity, u => u.MapFrom(g => g.NewsAgencyEntity == null ? "" : Translator.GetByKey(g.NewsAgencyEntity.EntityName)))
              .ForMember(f => f.NewsTypeEntity, u => u.MapFrom(g => g.NewsTypeEntity == null ? "" : Translator.GetByKey(g.NewsTypeEntity.EntityName)))
              .ForMember(f => f.NewsRelatedNewsList, g => g.MapFrom(t => t.NewsRelatedNewsNews))
              .ForMember(f => f.NewsFileList, g => g.MapFrom(u => u.NewsFile))
              .ForMember(f => f.NewsPropertyList, g => g.MapFrom(u => u.NewsProperty))
              .ForMember(f => f.NewsTagList, g => g.MapFrom(u => u.NewsTag.Where(f => !f.Tag.Deleted)))
              .ForMember(f => f.NewsCategoryList, g => g.MapFrom(u => u.NewsCategory.Select(x => x.Category)));

            CreateMap<News, BreakingNewsDto>()
                .ForMember(f => f.AuthorNameSurename, g => g.MapFrom(u => u.Author == null ? "" : u.Author.NameSurename))
                .ForMember(f => f.NewsId, u => u.MapFrom(g => g.Id))
                .ForMember(f => f.Url, u => u.MapFrom(g => g.GetUrl()));

            CreateMap<News, WideHeadingNewsDto>()
                .ForMember(f => f.AuthorNameSurename, g => g.MapFrom(u => u.Author == null ? "" : u.Author.NameSurename))
                .ForMember(f => f.NewsId, u => u.MapFrom(g => g.Id))
                .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFile != null &&
                                g.NewsFile.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.MainHeadingImage) ?
                                g.NewsFile.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.MainHeadingImage).File.GetFullFilePath() : null))
              .ForMember(f => f.Url, u => u.MapFrom(g => g.GetUrl()))
              .ForMember(f => f.NewsAgencyEntity, u => u.MapFrom(g => g.NewsAgencyEntity == null ? "" : Translator.GetByKey(g.NewsAgencyEntity.EntityName)))
              .ForMember(f => f.NewsTypeEntity, u => u.MapFrom(g => g.NewsTypeEntity == null ? "" : Translator.GetByKey(g.NewsTypeEntity.EntityName)))
              .ForMember(f => f.NewsRelatedNewsList, g => g.MapFrom(t => t.NewsRelatedNewsNews))
              .ForMember(f => f.NewsFileList, g => g.MapFrom(u => u.NewsFile))
              .ForMember(f => f.NewsPropertyList, g => g.MapFrom(u => u.NewsProperty))
              .ForMember(f => f.NewsTagList, g => g.MapFrom(u => u.NewsTag.Where(f => !f.Tag.Deleted)))
              .ForMember(f => f.NewsCategoryList, g => g.MapFrom(u => u.NewsCategory.Select(x => x.Category)));

            CreateMap<News, LiveNarrotationNewsDto>()
                .ForMember(f => f.AuthorNameSurename, g => g.MapFrom(u => u.Author == null ? "" : u.Author.NameSurename))
                .ForMember(f => f.NewsId, u => u.MapFrom(g => g.Id))
                .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFile != null &&
                                g.NewsFile.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
                                g.NewsFile.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.GetFullFilePath() : null))
              .ForMember(f => f.Url, u => u.MapFrom(g => g.GetUrl()))
              .ForMember(f => f.NewsAgencyEntity, u => u.MapFrom(g => g.NewsAgencyEntity == null ? "" : Translator.GetByKey(g.NewsAgencyEntity.EntityName)))
              .ForMember(f => f.NewsTypeEntity, u => u.MapFrom(g => g.NewsTypeEntity == null ? "" : Translator.GetByKey(g.NewsTypeEntity.EntityName)))
              .ForMember(f => f.NewsRelatedNewsList, g => g.MapFrom(t => t.NewsRelatedNewsNews))
              .ForMember(f => f.NewsFileList, g => g.MapFrom(u => u.NewsFile))
              .ForMember(f => f.NewsPropertyList, g => g.MapFrom(u => u.NewsProperty))
              .ForMember(f => f.NewsTagList, g => g.MapFrom(u => u.NewsTag.Where(f => !f.Tag.Deleted)))
              .ForMember(f => f.NewsCategoryList, g => g.MapFrom(u => u.NewsCategory.Select(x => x.Category)));

            CreateMap<News, SubHeadingDto>()
                .ForMember(f => f.AuthorNameSurename, g => g.MapFrom(u => u.Author == null ? "" : u.Author.NameSurename))
                .ForMember(f => f.NewsId, u => u.MapFrom(g => g.Id))
                .ForMember(f => f.Url, u => u.MapFrom(g => g.GetUrl()))
                .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFile != null &&
                                g.NewsFile.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.MainHeadingImage) ?
                                g.NewsFile.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.MainHeadingImage).File.GetFullFilePath() : null));

            CreateMap<News, MainHeadingDto>()
                .ForMember(f => f.AuthorNameSurename, g => g.MapFrom(u => u.Author == null ? "" : u.Author.NameSurename))
                .ForMember(f => f.NewsId, g => g.MapFrom(u => u.Id))
                .ForMember(f => f.Url, u => u.MapFrom(g => g.GetUrl()))
                .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFile != null &&
                                g.NewsFile.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.MainHeadingImage) ?
                                g.NewsFile.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.MainHeadingImage).File.GetFullFilePath() : null));

            CreateMap<News, MostViewedNewsDto>()
                .ForMember(f => f.Url, u => u.MapFrom(g => g.GetUrl()))
                .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFile != null &&
                                g.NewsFile.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
                                g.NewsFile.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.GetFullFilePath() : null))
                .ForMember(f => f.ViewCount, g => g.MapFrom(r => r.NewsCounter.Any(u => u.CounterEntityId == (int)NewsCounterEntities.TotalViewCount) ?
                r.NewsCounter.First(h => h.CounterEntityId == (int)NewsCounterEntities.TotalViewCount).Value : 0));

            CreateMap<News, MostHitNewsDto>()
                .ForMember(f => f.Url, u => u.MapFrom(g => g.GetUrl()))
                .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFile != null &&
                                g.NewsFile.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
                                g.NewsFile.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.GetFullFilePath() : null));

            CreateMap<News, MostSharedNewsDto>()
               .ForMember(f => f.AuthorNameSurename, g => g.MapFrom(u => u.Author == null ? "" : u.Author.NameSurename))
               .ForMember(f => f.Url, u => u.MapFrom(g => g.GetUrl()))
               .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFile != null &&
                               g.NewsFile.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
                               g.NewsFile.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.GetFullFilePath() : null))
               .ForMember(f => f.ShareCount, g => g.MapFrom(r => r.NewsCounter.Any(u => u.CounterEntityId == (int)NewsCounterEntities.TotalShareCount) ?
               r.NewsCounter.First(h => h.CounterEntityId == (int)NewsCounterEntities.TotalShareCount).Value : 0));

            CreateMap<News, MainPageNewsDto>()
                .ForMember(f => f.NewsId, u => u.MapFrom(g => g.Id))
                .ForMember(f => f.StyleCode, u => u.MapFrom(g => g.NewsCategory != null && g.NewsCategory.Any() ? g.NewsCategory.First().Category.StyleCode : ""))
                .ForMember(f => f.CategoryName, u => u.MapFrom(g => g.NewsCategory != null && g.NewsCategory.Any() ? g.NewsCategory.First().Category.CategoryName : ""))
                .ForMember(f => f.Url, u => u.MapFrom(g => g.GetUrl()))
                .ForMember(f => f.FileList, g => g.MapFrom(u => u.NewsFile));

            CreateMap<News, MainPageVideoNewsDto>()
                .ForMember(f => f.AuthorNameSurename, g => g.MapFrom(u => u.Author == null ? "" : u.Author.NameSurename))
                .ForMember(f => f.NewsId, u => u.MapFrom(g => g.Id))
                .ForMember(f => f.Url, u => u.MapFrom(g => g.GetUrl()))
                .ForMember(f => f.VideoUrl, u => u.MapFrom(g => g.NewsFile != null &&
                            g.NewsFile.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.GalleryVideo) ?
                            g.NewsFile.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.GalleryVideo).File.GetFullFilePath() : ""))
                .ForMember(f => f.VideoCoverImageUrl, u => u.MapFrom(g => g.NewsFile != null &&
                            g.NewsFile.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.GalleryVideo) ?
                            (
                            g.NewsFile.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.GalleryVideo).VideoCoverFile == null ? "" :
                            g.NewsFile.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.GalleryVideo).VideoCoverFile.GetFullFilePath()
                            ) : ""));

            CreateMap<News, MainPageImageNewsDto>()
                .ForMember(f => f.NewsId, u => u.MapFrom(g => g.Id))
                .ForMember(f => f.Url, u => u.MapFrom(g => g.GetUrl()))
                .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFile != null &&
                            g.NewsFile.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.GalleryPhoto) ?
                            g.NewsFile.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.GalleryPhoto).File.GetFullFilePath() : null));


            CreateMap<News, MainPageCategoryNewsDto>()
                .ForMember(f => f.AuthorNameSurename, g => g.MapFrom(u => u.Author == null ? "" : u.Author.NameSurename))
                .ForMember(f => f.NewsId, g => g.MapFrom(u => u.Id))
                .ForMember(f => f.Url, u => u.MapFrom(g => g.GetUrl()))
                .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFile != null &&
                            g.NewsFile.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
                            g.NewsFile.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.GetFullFilePath() : null));

            CreateMap<News, MainPageTagNewsDto>()
               .ForMember(f => f.AuthorNameSurename, g => g.MapFrom(u => u.Author == null ? "" : u.Author.NameSurename))
               .ForMember(f => f.NewsId, g => g.MapFrom(u => u.Id))
               .ForMember(f => f.Url, u => u.MapFrom(g => g.GetUrl()))
               .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFile != null &&
                           g.NewsFile.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
                           g.NewsFile.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.GetFullFilePath() : null));

            CreateMap<News, MainPageSearchNewsDto>()
               .ForMember(f => f.AuthorNameSurename, g => g.MapFrom(u => u.Author == null ? "" : u.Author.NameSurename))
               .ForMember(f => f.NewsId, g => g.MapFrom(u => u.Id))
               .ForMember(f => f.Url, u => u.MapFrom(g => g.GetUrl()))
               .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFile != null &&
                           g.NewsFile.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
                           g.NewsFile.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.GetFullFilePath() : null));

            CreateMap<News, NewsItem>()
              .ForMember(f => f.Url, u => u.MapFrom(g => g.GetUrl()))
              .ForMember(f => f.AgencyName, u => u.MapFrom(t => t.NewsAgencyEntity == null ? "MilliGazete" : Translator.GetByKey(t.NewsAgencyEntity.EntityName)))
              .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFile != null &&
                              g.NewsFile.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
                              g.NewsFile.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.GetFullFilePath() : null))
              .ForMember(f => f.Keywords, u => u.MapFrom(t => t.SeoKeywords))
              .ForMember(f => f.LastMod, u => u.MapFrom(t => t.CreatedAt))
              .ForMember(f => f.PulishDate, u => u.MapFrom(t => t.PublishDate))
              .ForMember(f => f.CategoryUrl, u => u.MapFrom(t => t.NewsCategory.Any(y => !y.Category.Deleted) ? t.NewsCategory.First(y => !y.Category.Deleted).Category.Url : ""));

            CreateMap<News, MainPageFourHillNewsDto>()
              .ForMember(f => f.AuthorNameSurename, g => g.MapFrom(u => u.Author == null ? "" : u.Author.NameSurename))
              .ForMember(f => f.NewsId, u => u.MapFrom(g => g.Id))
              .ForMember(f => f.Url, u => u.MapFrom(g => g.GetUrl()))
              .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFile != null &&
                              g.NewsFile.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
                              g.NewsFile.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.GetFullFilePath() : null));

            CreateMap<News, MainPageHeadlineSideNewsDto>()
             .ForMember(f => f.NewsId, u => u.MapFrom(g => g.Id))
             .ForMember(f => f.Url, u => u.MapFrom(g => g.GetUrl()))
             .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFile != null &&
                             g.NewsFile.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
                             g.NewsFile.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.GetFullFilePath() : null));

            CreateMap<News, MainPageArticleDto>()
               .ForMember(f => f.ArticleId, u => u.MapFrom(g => g.Id))
               .ForMember(f => f.Author, u => u.MapFrom(g => g.Author))
               .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFile != null &&
                           g.NewsFile.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
                           g.NewsFile.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.GetFullFilePath() : null));

            CreateMap<News, ArticleDto>()
                .ForMember(f => f.Url, g => g.MapFrom(t => t.GetUrl()))
              .ForMember(f => f.Author, u => u.MapFrom(g => g.Author))
              .ForMember(f => f.ReadCount, g => g.MapFrom(r => r.NewsCounter.Any(u => u.CounterEntityId == (int)NewsCounterEntities.TotalViewCount) ?
                r.NewsCounter.First(h => h.CounterEntityId == (int)NewsCounterEntities.TotalViewCount).Value : 0))
              .ForMember(f => f.ImageUrl, u => u.MapFrom(g => g.NewsFile != null &&
                          g.NewsFile.Any(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage) ?
                          g.NewsFile.First(t => t.NewsFileTypeEntityId == (int)NewsFileTypeEntities.NormalImage).File.GetFullFilePath() : null));

        }
    }
}

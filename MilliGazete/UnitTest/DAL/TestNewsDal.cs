using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Enums;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest.DAL
{
    public class TestNewsDal : TestMilliGazeteDbContext
    {
        public INewsDal newsDal;

        public TestNewsDal()
        {
            if (newsDal == null)
                newsDal = MockNewsDal();
        }

        INewsDal MockNewsDal()
        {
            var list = db.News.ToList();
            db.News.RemoveRange(list);
            for (int i = 1; i <= dataCount; i++)
            {
                db.News.Add(new News()
                {
                    Active = i < 8,
                    Id = i,
                    Title = "News Number " + i.ToString(),
                    PublishDate = DateTime.Now.AddDays(i * 5),
                    Approved = i < 8,
                    CreatedAt = DateTime.Now.AddDays(-i * 2),
                    Deleted = i > dataCount - 2,
                    HistoryNo = i,
                    HtmlContent = "<p>" + i + "</p>",
                    IsDraft = i % 2 == 0,
                    PublishTime = DateTime.Now.TimeOfDay,
                    Url = "Url " + i,
                    UpdatedAt = DateTime.Now.AddDays(i),
                    SocialTitle = "Social Title " + i,
                    SocialDescription = "Social Desc " + i,
                    ShortDescription = "Short Desc" + i,
                    SeoTitle = "Seo title " + i,
                    SeoKeywords = "keys " + i,
                    SeoDescription = "Seo Desc" + i,
                    AddUser = new User { FirstName = "fname", LastName = "lname", UserName = "username" },
                    AddUserId = 15,
                    NewsTag = new List<NewsTag> { new NewsTag { TagId = 15, NewsId = i, Tag = new Tag { Deleted = false, TagName = "tagname", Url = "url" } } },
                    IsLastNews = false,
                    NewsAgencyEntity = new Entity.Models.Entity { EntityName = "name" },
                    NewsAgencyEntityId = 1,
                    NewsCategory = new List<NewsCategory> { },
                    NewsComment = new List<NewsComment> { },
                    NewsCounter = new List<NewsCounter> { },
                    NewsFile = new List<NewsFile> { new NewsFile { NewsFileTypeEntityId = (int)NewsFileTypeEntities.NormalImage, File = new File { FileName = "", IsCdnFile = true } } },
                    NewsPosition = new List<NewsPosition> { },
                    NewsProperty = new List<NewsProperty> { },
                    NewsRelatedNewsNews = new List<NewsRelatedNews> { 
                        //new NewsRelatedNews {  NewsId = 15 , RelatedNewsId = 16 , 
                    //News = new News{ Title = "" },
                    //RelatedNews = new News{ Id  = 200000}
                    //} 
                    },
                    NewsRelatedNewsRelatedNews = new List<NewsRelatedNews> { },
                    NewsTypeEntity = new Entity.Models.Entity { EntityName = "" },
                    NewsTypeEntityId = 1,
                    UpdateUser = new User { FirstName = "fname", LastName = "lname", UserName = "username" },
                    UpdateUserId = 1
                });
                db.SaveChanges();
            }
            return new EfNewsDal(db);
        }
    }
}

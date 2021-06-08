using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entity.Enums;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest.DAL
{
    public class TestNewsBookmarkDal : TestMilliGazeteDbContext
    {
        public INewsBookmarkDal newsBookmarkDal;
        public TestNewsBookmarkDal()
        {
            if (newsBookmarkDal == null)
                newsBookmarkDal = MockNewsBookmarkDal();
        }

        INewsBookmarkDal MockNewsBookmarkDal()
        {
            var data = db.News.ToList();
            db.News.RemoveRange(data);

            var list = db.NewsBookmark.ToList();
            db.NewsBookmark.RemoveRange(list);

            var users = db.User.ToList();
            db.User.RemoveRange(users);


            for (int i = 1; i <= dataCount; i++)
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash("Password_" + i, out passwordHash, out passwordSalt);
                var user = new User()
                {
                    Active = i < 8,
                    Id = i,
                    CreatedAt = DateTime.Now.AddDays(-i * 2),
                    Deleted = i > dataCount - 2,
                    Email = "test" + i + "@test.com",
                    FirstName = "Fname " + i,
                    LastName = "Lname " + i,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    UserName = "User_" + i,
                    LastLoginDate = DateTime.Now.AddDays(-i),
                    LastLoginIpAddress = "192.168.1." + i
                };
                db.User.Add(user);


                var news = new News()
                {
                    Active = i < 8,
                    Id = i,
                    Title = "News Number " + i.ToString(),
                    PublishDate = DateTime.Now.AddDays(i * 5),
                    Approved = i < 8,
                    CreatedAt = DateTime.Now.AddDays(-i * 2),
                    Deleted = i > dataCount - 2,
                    HistoryNo =  i,
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
                    AddUser = user,
                    AddUserId = i,
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
                    NewsRelatedNewsNews = new List<NewsRelatedNews>
                    {
                        //new NewsRelatedNews {  NewsId = 15 , RelatedNewsId = 16 , 
                        //News = new News{ Title = "" },
                        //RelatedNews = new News{ Id  = 200000}
                        //} 
                    },
                    NewsRelatedNewsRelatedNews = new List<NewsRelatedNews> { },
                    NewsTypeEntity = new Entity.Models.Entity { EntityName = "" },
                    NewsTypeEntityId = 1,
                    UpdateUser = user,
                    UpdateUserId = i
                };
                db.News.Add(news);

                db.NewsBookmark.Add(new NewsBookmark()
                {
                    Id = i,
                    NewsId = i,
                    UserId = i,
                    News = news,
                    User = user
                });
                db.SaveChanges();
            }



            for (int i = 1; i <= dataCount; i++)
            {

                db.SaveChanges();
            }
            return new EfNewsBookmarkDal(db);
        }


    }
}

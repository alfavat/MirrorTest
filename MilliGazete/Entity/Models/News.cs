using System;
using System.Collections.Generic;

namespace Entity.Models
{
    public partial class News
    {
        public News()
        {
            NewsBookmark = new HashSet<NewsBookmark>();
            NewsCategory = new HashSet<NewsCategory>();
            NewsComment = new HashSet<NewsComment>();
            NewsCounter = new HashSet<NewsCounter>();
            NewsFile = new HashSet<NewsFile>();
            NewsHit = new HashSet<NewsHit>();
            NewsHitDetail = new HashSet<NewsHitDetail>();
            NewsPosition = new HashSet<NewsPosition>();
            NewsProperty = new HashSet<NewsProperty>();
            NewsRelatedNewsNews = new HashSet<NewsRelatedNews>();
            NewsRelatedNewsRelatedNews = new HashSet<NewsRelatedNews>();
            NewsTag = new HashSet<NewsTag>();
        }

        public int Id { get; set; }
        public int AddUserId { get; set; }
        public int? UpdateUserId { get; set; }
        public int? NewsTypeEntityId { get; set; }
        public int? NewsAgencyEntityId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string HtmlContent { get; set; }
        public string SocialTitle { get; set; }
        public string SocialDescription { get; set; }
        public string SeoTitle { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        public string Url { get; set; }
        public DateTime? PublishDate { get; set; }
        public TimeSpan? PublishTime { get; set; }
        public bool IsDraft { get; set; }
        public bool? Approved { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Deleted { get; set; }
        public bool IsLastNews { get; set; }
        public int? HistoryNo { get; set; }
        public string InnerTitle { get; set; }
        public int? AuthorId { get; set; }

        public virtual User AddUser { get; set; }
        public virtual Author Author { get; set; }
        public virtual Entity NewsAgencyEntity { get; set; }
        public virtual Entity NewsTypeEntity { get; set; }
        public virtual User UpdateUser { get; set; }
        public virtual ICollection<NewsBookmark> NewsBookmark { get; set; }
        public virtual ICollection<NewsCategory> NewsCategory { get; set; }
        public virtual ICollection<NewsComment> NewsComment { get; set; }
        public virtual ICollection<NewsCounter> NewsCounter { get; set; }
        public virtual ICollection<NewsFile> NewsFile { get; set; }
        public virtual ICollection<NewsHit> NewsHit { get; set; }
        public virtual ICollection<NewsHitDetail> NewsHitDetail { get; set; }
        public virtual ICollection<NewsPosition> NewsPosition { get; set; }
        public virtual ICollection<NewsProperty> NewsProperty { get; set; }
        public virtual ICollection<NewsRelatedNews> NewsRelatedNewsNews { get; set; }
        public virtual ICollection<NewsRelatedNews> NewsRelatedNewsRelatedNews { get; set; }
        public virtual ICollection<NewsTag> NewsTag { get; set; }
    }
}

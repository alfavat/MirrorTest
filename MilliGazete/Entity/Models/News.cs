using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class News
    {
        public News()
        {
            NewsBookmarks = new HashSet<NewsBookmark>();
            NewsCategories = new HashSet<NewsCategory>();
            NewsComments = new HashSet<NewsComment>();
            NewsCounters = new HashSet<NewsCounter>();
            NewsFiles = new HashSet<NewsFile>();
            NewsHitDetails = new HashSet<NewsHitDetail>();
            NewsHits = new HashSet<NewsHit>();
            NewsPositions = new HashSet<NewsPosition>();
            NewsProperties = new HashSet<NewsProperty>();
            NewsRelatedNewsNews = new HashSet<NewsRelatedNews>();
            NewsRelatedNewsRelatedNews = new HashSet<NewsRelatedNews>();
            NewsTags = new HashSet<NewsTag>();
        }

        public int Id { get; set; }
        public int AddUserId { get; set; }
        public int? UpdateUserId { get; set; }
        public int? NewsTypeEntityId { get; set; }
        public int? NewsAgencyEntityId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
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
        public int? ReporterId { get; set; }
        public bool? UseTitle { get; set; }

        public virtual User AddUser { get; set; }
        public virtual Author Author { get; set; }
        public virtual Entity NewsAgencyEntity { get; set; }
        public virtual Entity NewsTypeEntity { get; set; }
        public virtual Reporter Reporter { get; set; }
        public virtual User UpdateUser { get; set; }
        public virtual ICollection<NewsBookmark> NewsBookmarks { get; set; }
        public virtual ICollection<NewsCategory> NewsCategories { get; set; }
        public virtual ICollection<NewsComment> NewsComments { get; set; }
        public virtual ICollection<NewsCounter> NewsCounters { get; set; }
        public virtual ICollection<NewsFile> NewsFiles { get; set; }
        public virtual ICollection<NewsHitDetail> NewsHitDetails { get; set; }
        public virtual ICollection<NewsHit> NewsHits { get; set; }
        public virtual ICollection<NewsPosition> NewsPositions { get; set; }
        public virtual ICollection<NewsProperty> NewsProperties { get; set; }
        public virtual ICollection<NewsRelatedNews> NewsRelatedNewsNews { get; set; }
        public virtual ICollection<NewsRelatedNews> NewsRelatedNewsRelatedNews { get; set; }
        public virtual ICollection<NewsTag> NewsTags { get; set; }
    }
}

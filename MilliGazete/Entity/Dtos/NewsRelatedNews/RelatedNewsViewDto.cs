using System;

namespace Entity.Dtos
{
    public class RelatedNewsViewDto
    {
        public int Id { get; set; }
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
        public int HistoryNo { get; set; }
        public bool? IsDraft { get; set; }
        public bool? Approved { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public string AddUser { get; set; }
        public string NewsAgencyEntity { get; set; }
        public string NewsTypeEntity { get; set; }
        public string UpdateUser { get; set; }
        public string Thumbnail { get; set; }

        public int UserId { get; set; }
        public int? NewsTypeEntityId { get; set; }
        public int? NewsAgencyEntityId { get; set; }
    }
}

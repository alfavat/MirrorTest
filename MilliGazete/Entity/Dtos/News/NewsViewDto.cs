using System;
using System.Collections.Generic;

namespace Entity.Dtos
{
    public class NewsViewDto
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
        public string InnerTitle { get; set; }
        public string Url { get; set; }
        public string ExternalLink { get; set; }
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
        public int? AuthorId { get; set; }
        public int? ReporterId { get; set; }
        public int? NewsHitCount { get; set; }
        public bool UseTitle { get; set; }

        public AuthorDto Author { get; set; }
        public ReporterDto Reporter { get; set; }
        public List<NewsCategoryDto> NewsCategoryList { get; set; }
        public List<NewsFileDto> NewsFileList { get; set; }
        public List<NewsPositionDto> NewsPositionList { get; set; }
        public List<NewsPropertyDto> NewsPropertyList { get; set; }
        public List<NewsRelatedNewsViewDto> NewsRelatedNewsList { get; set; }
        public List<NewsTagDto> NewsTagList { get; set; }
    }
}

using System;

namespace Entity.Dtos
{
    public class NewsPagingViewDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string HtmlContent { get; set; }
        public string Url { get; set; }
        public DateTime? PublishDate { get; set; }
        public TimeSpan? PublishTime { get; set; }
        public bool Active { get; set; }
        public bool? IsDraft { get; set; }
        public string AddUser { get; set; }
        public string NewsAgencyEntity { get; set; }
        public string NewsTypeEntity { get; set; }
        public string Thumbnail { get; set; }
        public int? NewsHitCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

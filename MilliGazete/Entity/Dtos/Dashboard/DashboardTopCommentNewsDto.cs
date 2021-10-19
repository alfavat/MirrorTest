using System;

namespace Entity.Dtos
{
    public class DashboardTopCommentNewsDto
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Url { get; set; }
        public string ExternalLink { get; set; }
        public int CommentsCount { get; set; }
        public DateTime? PublishDate { get; set; }
        public int TotalViewCount { get; set; }
    }
}

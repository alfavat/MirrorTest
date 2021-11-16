using System.Collections.Generic;

namespace Entity.Dtos
{
    public class NewsAddDto
    {
        public int NewsId { get; set; }
        public int UserId { get; set; } // if id>0 updateuserid else adduserid
        public int? NewsTypeEntityId { get; set; }
        public int? NewsAgencyEntityId { get; set; }
        public string Title { get; set; } // 250
        public string SubTitle { get; set; }
        public string ShortDescription { get; set; }
        public string HtmlContent { get; set; }
        public string InnerTitle { get; set; }
        public string SocialTitle { get; set; } // 250
        public string SocialDescription { get; set; }
        public string SeoTitle { get; set; } // 250
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        public string Url { get; set; } // 250
        public string PublishDate { get; set; }
        public string PublishTime { get; set; }
        public bool IsDraft { get; set; }
        public bool Active { get; set; }
        public bool PushNotification { get; set; } = false;
        public int? AuthorId { get; set; }
        public int? ReporterId { get; set; }
        public bool UseTitle { get; set; }

        public List<NewsCategoryAddDto> NewsCategoryList { get; set; }
        public List<NewsFileAddDto> NewsFileList { get; set; }
        public List<NewsPositionAddDto> NewsPositionList { get; set; }
        public List<NewsPropertyAddDto> NewsPropertyList { get; set; }
        public List<NewsRelatedNewsAddDto> NewsRelatedNewsList { get; set; }
        public List<NewsTagAddDto> NewsTagList { get; set; }
    }
}

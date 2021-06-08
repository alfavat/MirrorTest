using System;
using System.Collections.Generic;

namespace Entity.Dtos
{
    public class LiveNarrotationNewsDto
    {
        public int NewsId { get; set; }
        public string AuthorNameSurename { get; set; }
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
        public DateTime? PublishDate { get; set; }
        public TimeSpan? PublishTime { get; set; }
        public string NewsAgencyEntity { get; set; }
        public string NewsTypeEntity { get; set; }

        public int UserId { get; set; }
        public int? NewsTypeEntityId { get; set; }
        public int? NewsAgencyEntityId { get; set; }
        public bool BookMarkStatus { get; set; }
        public string ImageUrl { get; set; }


        public List<CategoryDto> NewsCategoryList { get; set; }
        public List<MainPageNewsFileDto> NewsFileList { get; set; }
        public List<NewsPropertyDto> NewsPropertyList { get; set; }
        public List<MainPageRelatedNewsDto> NewsRelatedNewsList { get; set; }
        public List<NewsTagDto> NewsTagList { get; set; }
    }
}

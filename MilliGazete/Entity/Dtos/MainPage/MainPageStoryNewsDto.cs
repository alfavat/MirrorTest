using System;
using System.Collections.Generic;

namespace Entity.Dtos
{
    public class MainPageStoryNewsDto
    {
        public string CategoryName { get; set; }
        public string Thumbnail { get; set; }
        public List<StoryNewsDetail> Data { get; set; }
    }

    public class StoryNewsDetail
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ExternalLink { get; set; }
        public DateTime StoryDate { get; set; }
        public TimeSpan StoryTime { get; set; }
        public string MainImage { get; set; }
    }
}

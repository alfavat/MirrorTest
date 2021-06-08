using System;

namespace Entity.Dtos
{
    public class NewsItem
    {
        public string Url { get; set; }
        public DateTime LastMod { get; set; }
        public string AgencyName { get; set; }
        public string Title { get; set; }
        public string Keywords { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PulishDate { get; set; }
        public string CategoryUrl { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Entity.Dtos
{
    public class SiteMapItem
    {
        public string Loc { get; set; }
        public string ChangeFreq { get; set; }
        public DateTime LastMod { get; set; }
        public string Priority { get; set; }
        public List<NewsItem> News { get; set; }
    }
}

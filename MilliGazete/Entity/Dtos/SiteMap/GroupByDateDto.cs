using System.Collections.Generic;

namespace Entity.Dtos
{
    public class GroupByDateDto
    {
        public string YearAndMonth { get; set; }
        public List<NewsItem> NewsList { get; set; }
    }
}

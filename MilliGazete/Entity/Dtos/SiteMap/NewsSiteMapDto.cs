using System.Collections.Generic;

namespace Entity.Dtos
{
    public class NewsSiteMapDto
    {
        public string CategoryName { get; set; }
        public List<GroupByDateDto> Items { get; set; }
    }
}

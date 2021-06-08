using Entity.Dtos;
using System.Collections.Generic;

namespace WebUI.Models
{
    public class SearchNews
    {
        public string Query { get; set; }
        public List<MainPageSearchNewsDto> Data { get; set; }
        public int TotalCount { get; set; }
        public int Limit { get; set; }
    }
}

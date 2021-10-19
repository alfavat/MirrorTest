using System.Collections.Generic;

namespace Entity.Dtos
{
    public class MainPageNewsDto
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string ExternalLink { get; set; }
        public string CategoryName { get; set; }
        public string ShortDescription { get; set; }
        public string StyleCode { get; set; }
        public List<MainPageNewsFileDto> FileList { get; set; }
    }
}

using System;

namespace Entity.Dtos
{
    public class NewsHistoryDto
    {
        public int NewsId { get; set; }
        public int HistoryNo { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
    }
}

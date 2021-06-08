using System;

namespace Entity.Dtos
{
    public class NewsHitDetailDto
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public int? UserId { get; set; }
        public string IpAddress { get; set; }
        public int NewsHitComeFromEntityId { get; set; }
        public DateTime CreatedAt { get; set; }

        public MostHitNewsDto News { get; set; }
        public string UserFullName { get; set; }
    }
}

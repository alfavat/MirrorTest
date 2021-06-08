using Entity.Enums;
using System;
using System.Collections.Generic;

namespace Entity.Dtos
{
    public class NewsAgencyAddDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ParentCategory { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public string LastMinute { get; set; }
        public DateTime? PublishDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? ImageUpdateDate { get; set; }
        public int? NewsId { get; set; }
        public string Country { get; set; }
        public DateTime? VideoUpdateDate { get; set; }
        public string Code { get; set; }
        public NewsAgencyEntities NewsAgencyEntityId { get; set; }
        public List<AgencyNewsImageDto> Images { get; set; }
        public List<AgencyNewsVideoDto> Videos { get; set; }
    }
}

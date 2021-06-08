using System;
using System.Collections.Generic;

namespace Entity.Models
{
    public partial class AgencyNews
    {
        public AgencyNews()
        {
            AgencyNewsFile = new HashSet<AgencyNewsFile>();
        }

        public long Id { get; set; }
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
        public int NewsAgencyEntityId { get; set; }

        public virtual ICollection<AgencyNewsFile> AgencyNewsFile { get; set; }
    }
}

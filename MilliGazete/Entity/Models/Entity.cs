using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class Entity
    {
        public Entity()
        {
            Categories = new HashSet<Category>();
            NewsCounters = new HashSet<NewsCounter>();
            NewsFiles = new HashSet<NewsFile>();
            NewsNewsAgencyEntities = new HashSet<News>();
            NewsNewsTypeEntities = new HashSet<News>();
            NewsPositions = new HashSet<NewsPosition>();
            NewsProperties = new HashSet<NewsProperty>();
        }

        public int Id { get; set; }
        public int EntityGroupId { get; set; }
        public string EntityName { get; set; }

        public virtual EntityGroup EntityGroup { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<NewsCounter> NewsCounters { get; set; }
        public virtual ICollection<NewsFile> NewsFiles { get; set; }
        public virtual ICollection<News> NewsNewsAgencyEntities { get; set; }
        public virtual ICollection<News> NewsNewsTypeEntities { get; set; }
        public virtual ICollection<NewsPosition> NewsPositions { get; set; }
        public virtual ICollection<NewsProperty> NewsProperties { get; set; }
    }
}

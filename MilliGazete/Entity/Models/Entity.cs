using System;
using System.Collections.Generic;

namespace Entity.Models
{
    public partial class Entity
    {
        public Entity()
        {
            Category = new HashSet<Category>();
            NewsCounter = new HashSet<NewsCounter>();
            NewsFile = new HashSet<NewsFile>();
            NewsNewsAgencyEntity = new HashSet<News>();
            NewsNewsTypeEntity = new HashSet<News>();
            NewsPosition = new HashSet<NewsPosition>();
            NewsProperty = new HashSet<NewsProperty>();
        }

        public int Id { get; set; }
        public int EntityGroupId { get; set; }
        public string EntityName { get; set; }

        public virtual EntityGroup EntityGroup { get; set; }
        public virtual ICollection<Category> Category { get; set; }
        public virtual ICollection<NewsCounter> NewsCounter { get; set; }
        public virtual ICollection<NewsFile> NewsFile { get; set; }
        public virtual ICollection<News> NewsNewsAgencyEntity { get; set; }
        public virtual ICollection<News> NewsNewsTypeEntity { get; set; }
        public virtual ICollection<NewsPosition> NewsPosition { get; set; }
        public virtual ICollection<NewsProperty> NewsProperty { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Entity.Models
{
    public partial class EntityGroup
    {
        public EntityGroup()
        {
            Entity = new HashSet<Entity>();
        }

        public int Id { get; set; }
        public string EntityGroupName { get; set; }

        public virtual ICollection<Entity> Entity { get; set; }
    }
}

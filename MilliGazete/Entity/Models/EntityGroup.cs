using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class EntityGroup
    {
        public EntityGroup()
        {
            Entities = new HashSet<Entity>();
        }

        public int Id { get; set; }
        public string EntityGroupName { get; set; }

        public virtual ICollection<Entity> Entities { get; set; }
    }
}

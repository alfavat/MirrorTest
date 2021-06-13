using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class District
    {
        public District()
        {
            Subscriptions = new HashSet<Subscription>();
        }

        public int Id { get; set; }
        public int CityId { get; set; }
        public string Name { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}

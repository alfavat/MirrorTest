using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class Subscription
    {
        public int Id { get; set; }
        public int? CityId { get; set; }
        public int DistrictId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual City City { get; set; }
        public virtual District District { get; set; }
    }
}

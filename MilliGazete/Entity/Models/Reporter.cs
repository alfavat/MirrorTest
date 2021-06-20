using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class Reporter
    {
        public Reporter()
        {
            News = new HashSet<News>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public int? ProfileImageId { get; set; }
        public bool Deleted { get; set; }

        public virtual File ProfileImage { get; set; }
        public virtual ICollection<News> News { get; set; }
    }
}

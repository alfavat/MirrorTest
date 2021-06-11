using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class AgencyNewsFile
    {
        public int Id { get; set; }
        public long AgencyNewsId { get; set; }
        public int? FileId { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string FileType { get; set; }

        public virtual AgencyNews AgencyNews { get; set; }
        public virtual File File { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Entity.Models
{
    public partial class NewsFile
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public int FileId { get; set; }
        public int? VideoCoverFileId { get; set; }
        public int NewsFileTypeEntityId { get; set; }
        public int? Order { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual File File { get; set; }
        public virtual News News { get; set; }
        public virtual Entity NewsFileTypeEntity { get; set; }
        public virtual File VideoCoverFile { get; set; }
    }
}

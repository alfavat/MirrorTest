using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class NewsCommentLike
    {
        public int Id { get; set; }
        public int NewsCommentId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool? IsLike { get; set; }

        public virtual NewsComment NewsComment { get; set; }
        public virtual User User { get; set; }
    }
}

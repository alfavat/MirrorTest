using System;
using System.Collections.Generic;

namespace Entity.Models
{
    public partial class NewsComment
    {
        public NewsComment()
        {
            NewsCommentLike = new HashSet<NewsCommentLike>();
        }

        public int Id { get; set; }
        public int NewsId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? TotalLikeCount { get; set; }
        public bool Approved { get; set; }
        public bool Deleted { get; set; }

        public virtual News News { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<NewsCommentLike> NewsCommentLike { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class NewsComment
    {
        public NewsComment()
        {
            NewsCommentLikes = new HashSet<NewsCommentLike>();
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
        public int? InitialLikeCount { get; set; }
        public string IpAddress { get; set; }
        public string AnonymousUsername { get; set; }

        public virtual News News { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<NewsCommentLike> NewsCommentLikes { get; set; }
    }
}

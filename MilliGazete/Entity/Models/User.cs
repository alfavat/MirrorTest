using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class User
    {
        public User()
        {
            Files = new HashSet<File>();
            Logs = new HashSet<Log>();
            NewsAddUsers = new HashSet<News>();
            NewsBookmarks = new HashSet<NewsBookmark>();
            NewsCommentLikes = new HashSet<NewsCommentLike>();
            NewsComments = new HashSet<NewsComment>();
            NewsHitDetails = new HashSet<NewsHitDetail>();
            NewsUpdateUsers = new HashSet<News>();
            UserCategoryRelations = new HashSet<UserCategoryRelation>();
            UserOperationClaims = new HashSet<UserOperationClaim>();
            UserPasswordRequests = new HashSet<UserPasswordRequest>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string LastLoginIpAddress { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Deleted { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool IsEmployee { get; set; }

        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
        public virtual ICollection<News> NewsAddUsers { get; set; }
        public virtual ICollection<NewsBookmark> NewsBookmarks { get; set; }
        public virtual ICollection<NewsCommentLike> NewsCommentLikes { get; set; }
        public virtual ICollection<NewsComment> NewsComments { get; set; }
        public virtual ICollection<NewsHitDetail> NewsHitDetails { get; set; }
        public virtual ICollection<News> NewsUpdateUsers { get; set; }
        public virtual ICollection<UserCategoryRelation> UserCategoryRelations { get; set; }
        public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }
        public virtual ICollection<UserPasswordRequest> UserPasswordRequests { get; set; }
    }
}

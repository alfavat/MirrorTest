using System;
using System.Collections.Generic;

namespace Entity.Models
{
    public partial class User
    {
        public User()
        {
            File = new HashSet<File>();
            Log = new HashSet<Log>();
            NewsAddUser = new HashSet<News>();
            NewsBookmark = new HashSet<NewsBookmark>();
            NewsComment = new HashSet<NewsComment>();
            NewsCommentLike = new HashSet<NewsCommentLike>();
            NewsHitDetail = new HashSet<NewsHitDetail>();
            NewsUpdateUser = new HashSet<News>();
            UserCategoryRelation = new HashSet<UserCategoryRelation>();
            UserOperationClaim = new HashSet<UserOperationClaim>();
            UserPasswordRequest = new HashSet<UserPasswordRequest>();
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

        public virtual ICollection<File> File { get; set; }
        public virtual ICollection<Log> Log { get; set; }
        public virtual ICollection<News> NewsAddUser { get; set; }
        public virtual ICollection<NewsBookmark> NewsBookmark { get; set; }
        public virtual ICollection<NewsComment> NewsComment { get; set; }
        public virtual ICollection<NewsCommentLike> NewsCommentLike { get; set; }
        public virtual ICollection<NewsHitDetail> NewsHitDetail { get; set; }
        public virtual ICollection<News> NewsUpdateUser { get; set; }
        public virtual ICollection<UserCategoryRelation> UserCategoryRelation { get; set; }
        public virtual ICollection<UserOperationClaim> UserOperationClaim { get; set; }
        public virtual ICollection<UserPasswordRequest> UserPasswordRequest { get; set; }
    }
}

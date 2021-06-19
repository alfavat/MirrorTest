using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class File
    {
        public File()
        {
            AgencyNewsFiles = new HashSet<AgencyNewsFile>();
            AuthorFeaturedImageFiles = new HashSet<Author>();
            AuthorPhotoFiles = new HashSet<Author>();
            Categories = new HashSet<Category>();
            NewsFileFiles = new HashSet<NewsFile>();
            NewsFileVideoCoverFiles = new HashSet<NewsFile>();
            Pages = new HashSet<Page>();
            Reporters = new HashSet<Reporter>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public double? FileSizeKb { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public bool? IsCdnFile { get; set; }
        public bool? VideoSound { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Deleted { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<AgencyNewsFile> AgencyNewsFiles { get; set; }
        public virtual ICollection<Author> AuthorFeaturedImageFiles { get; set; }
        public virtual ICollection<Author> AuthorPhotoFiles { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<NewsFile> NewsFileFiles { get; set; }
        public virtual ICollection<NewsFile> NewsFileVideoCoverFiles { get; set; }
        public virtual ICollection<Page> Pages { get; set; }
        public virtual ICollection<Reporter> Reporters { get; set; }
    }
}

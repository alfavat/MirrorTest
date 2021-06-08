using System;
using System.Collections.Generic;

namespace Entity.Models
{
    public partial class File
    {
        public File()
        {
            AgencyNewsFile = new HashSet<AgencyNewsFile>();
            AuthorFeaturedImageFile = new HashSet<Author>();
            AuthorPhotoFile = new HashSet<Author>();
            Category = new HashSet<Category>();
            NewsFileFile = new HashSet<NewsFile>();
            NewsFileVideoCoverFile = new HashSet<NewsFile>();
            Page = new HashSet<Page>();
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
        public virtual ICollection<AgencyNewsFile> AgencyNewsFile { get; set; }
        public virtual ICollection<Author> AuthorFeaturedImageFile { get; set; }
        public virtual ICollection<Author> AuthorPhotoFile { get; set; }
        public virtual ICollection<Category> Category { get; set; }
        public virtual ICollection<NewsFile> NewsFileFile { get; set; }
        public virtual ICollection<NewsFile> NewsFileVideoCoverFile { get; set; }
        public virtual ICollection<Page> Page { get; set; }
    }
}

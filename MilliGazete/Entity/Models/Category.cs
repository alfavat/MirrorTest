using System;
using System.Collections.Generic;

namespace Entity.Models
{
    public partial class Category
    {
        public Category()
        {
            InverseParentCategory = new HashSet<Category>();
            NewsCategory = new HashSet<NewsCategory>();
            UserCategoryRelation = new HashSet<UserCategoryRelation>();
        }

        public int Id { get; set; }
        public int? ParentCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Symbol { get; set; }
        public string StyleCode { get; set; }
        public string Url { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        public bool? IsStatic { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Deleted { get; set; }
        public int? HeadingPositionEntityId { get; set; }
        public int? FeaturedImageFileId { get; set; }

        public virtual File FeaturedImageFile { get; set; }
        public virtual Entity HeadingPositionEntity { get; set; }
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Category> InverseParentCategory { get; set; }
        public virtual ICollection<NewsCategory> NewsCategory { get; set; }
        public virtual ICollection<UserCategoryRelation> UserCategoryRelation { get; set; }
    }
}

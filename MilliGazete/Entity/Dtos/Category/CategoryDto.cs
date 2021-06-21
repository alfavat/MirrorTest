using System;

namespace Entity.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public int? ParentCategoryId { get; set; }
        public string ParentCategoryName { get; set; }
        public string CategoryName { get; set; }
        public string Symbol { get; set; }
        public string StyleCode { get; set; }
        public string Url { get; set; }
        public string SeoKeywords { get; set; }
        public string SeoDescription { get; set; }
        public bool? IsStatic { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? HeadingPositionEntityId { get; set; }
        public int LanguageId { get; set; }

        public FileDto FeaturedImageFile { get; set; }
    }
}

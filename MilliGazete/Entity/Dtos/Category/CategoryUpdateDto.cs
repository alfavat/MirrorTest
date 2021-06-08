namespace Entity.Dtos
{
    public class CategoryUpdateDto
    {
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


        public int? HeadingPositionEntityId { get; set; }
        public int? FeaturedImageFileId { get; set; }
    }
}

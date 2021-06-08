using System;

namespace Entity.Dtos
{
    public class CategoryPagingDto : PagingDto
    {
        public bool? Active { get; set; }
        public int? ParentCategoryId { get; set; }
        public bool? IsStatic { get; set; }
        public DateTime? FromCreatedAt { get; set; }
        public DateTime? ToCreatedAt { get; set; }
    }
}

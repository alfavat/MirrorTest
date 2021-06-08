using System;
using System.Collections.Generic;

namespace Entity.Dtos
{
    public class MenuViewDto
    {
        public int Id { get; set; }
        public int? ParentMenuId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Active { get; set; }

        public string ParentMenuTitle { get; set; }
        public List<MenuViewDto> ChildrenMenuList { get; set; }
    }
}

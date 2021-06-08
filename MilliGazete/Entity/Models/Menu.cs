using System;
using System.Collections.Generic;

namespace Entity.Models
{
    public partial class Menu
    {
        public Menu()
        {
            InverseParentMenu = new HashSet<Menu>();
        }

        public int Id { get; set; }
        public int? ParentMenuId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Deleted { get; set; }

        public virtual Menu ParentMenu { get; set; }
        public virtual ICollection<Menu> InverseParentMenu { get; set; }
    }
}

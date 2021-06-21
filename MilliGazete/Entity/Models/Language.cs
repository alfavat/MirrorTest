using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class Language
    {
        public Language()
        {
            Categories = new HashSet<Category>();
            Menus = new HashSet<Menu>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
    }
}

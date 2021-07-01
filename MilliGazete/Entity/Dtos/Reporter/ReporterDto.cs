using Entity.Models;
using System;
using System.Collections.Generic;

namespace Entity.Dtos
{
    public class ReporterDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int? ProfileImageId { get; set; }

        public virtual FileDto ProfileImage { get; set; }
    }
}

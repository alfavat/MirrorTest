using System.Collections.Generic;

namespace Entity.Dtos
{
    public class ReporterUpdateDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int? ProfileImageId { get; set; }
    }
}

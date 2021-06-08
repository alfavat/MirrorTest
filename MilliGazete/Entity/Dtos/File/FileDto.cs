using System;

namespace Entity.Dtos
{
    public class FileDto
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public double? FileSizeKb { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public bool? IsCdnFile { get; set; }
        public bool? VideoSound { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

namespace Entity.Dtos
{
    public class NewsFileDto
    {
        public int Id { get; set; }
        public int FileId { get; set; }
        public int? VideoCoverFileId { get; set; }
        public int NewsFileTypeEntityId { get; set; }
        public int? Order { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public string CoverFileName { get; set; }
        public bool CameFromPool { get; set; }
        public string NewsTitle { get; set; }
    }
}

namespace Entity.Dtos
{
    public class NewsFileAddDto
    {
        public int FileId { get; set; }
        public int? VideoCoverFileId { get; set; }
        public int NewsFileTypeEntityId { get; set; }
        public int? Order { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}

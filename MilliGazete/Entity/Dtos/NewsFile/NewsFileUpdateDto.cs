namespace Entity.Dtos
{
    public class NewsFileUpdateDto
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public int FileId { get; set; }
        public int? VideoCoverFileId { get; set; }
        public int NewsFileTypeEntityId { get; set; }
        public int? Order { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}

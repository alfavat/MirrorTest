namespace Entity.Dtos
{
    public class MainPageNewsFileDto
    {

        public int? Order { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public string CoverFileName { get; set; }
        public int NewsFileTypeEntityId { get; set; }
        public virtual Models.Entity NewsFileTypeEntity { get; set; }
    }
}

namespace Entity.Dtos
{
    public class MenuAddDto
    {
        public int? ParentMenuId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int LanguageId { get; set; }
    }
}

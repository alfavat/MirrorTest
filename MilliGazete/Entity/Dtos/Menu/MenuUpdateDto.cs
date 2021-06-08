namespace Entity.Dtos
{
    public class MenuUpdateDto
    {
        public int Id { get; set; }
        public int? ParentMenuId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public bool Active { get; set; }
    }
}

namespace Entity.Dtos
{
    public class TagUpdateDto
    {
        public int Id { get; set; }
        public string TagName { get; set; }
        public string Url { get; set; }
        public bool Active { get; set; }
    }
}

namespace Entity.Dtos
{
    public class NewsPropertyUpdateDto
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public int PropertyEntityId { get; set; }
        public bool? Value { get; set; }
    }
}

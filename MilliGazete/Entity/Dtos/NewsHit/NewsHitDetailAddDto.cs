namespace Entity.Dtos
{
    public class NewsHitDetailAddDto
    {
        public int NewsId { get; set; }
        public int NewsHitComeFromEntityId { get; set; }
        public string IpAddress { get; set; }
        public int? UserId { get; set; }
    }
}

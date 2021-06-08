namespace Entity.Dtos
{
    public class NewsPositionDto
    {
        public int PositionEntityId { get; set; }
        public int NewsId { get; set; }
        public bool? Value { get; set; }
        public int? Order { get; set; }
        public NewsPositionNewsDto News { get; set; }

    }
}

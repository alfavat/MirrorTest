namespace Entity.Dtos
{
    public class ReporterDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int? ProfileImageId { get; set; }
        public string Url { get; set; }

        public string ProfileImage { get; set; }
    }
}

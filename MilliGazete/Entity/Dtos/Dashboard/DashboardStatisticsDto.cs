namespace Entity.Dtos
{
    public class DashboardStatisticsDto
    {
        public int TotalUserCount { get; set; }
        public int TotalNewsReadCounter { get; set; }
        public int TodayNewsReadCounter { get; set; }
        public int TotalNewsCount { get; set; }
        public int TodayCreatedNewsCount { get; set; }
        public int TotalPhotoGalleryCount { get; set; }
        public int TodayCreatedPhotoGalleryCount { get; set; }
        public int TotalVideoGalleryCount { get; set; }
        public int TodayCreatedVideoGalleryCount { get; set; }
        public int TotalCommentCount { get; set; }
        public int TodayCreatedCommentCount { get; set; }
    }
}

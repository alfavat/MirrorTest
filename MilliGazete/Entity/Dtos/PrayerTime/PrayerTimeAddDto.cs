namespace Entity.Dtos
{
    public class PrayerTimeAddDto
    {
        public int CityId { get; set; }
        public string PrayerDate { get; set; }
        public string DawnTime { get; set; }
        public string SunTime { get; set; }
        public string NoonPrayer { get; set; }
        public string AfternoonPrayer { get; set; }
        public string EveningPrayer { get; set; }
        public string NightPrayer { get; set; }
    }
}

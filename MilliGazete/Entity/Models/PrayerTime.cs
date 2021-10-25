using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class PrayerTime
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public DateTime PrayerDate { get; set; }
        public TimeSpan DawnTime { get; set; }
        public TimeSpan SunTime { get; set; }
        public TimeSpan NoonPrayer { get; set; }
        public TimeSpan AfternoonPrayer { get; set; }
        public TimeSpan EveningPrayer { get; set; }
        public TimeSpan NightPrayer { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public virtual City City { get; set; }
    }
}

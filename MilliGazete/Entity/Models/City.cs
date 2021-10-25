using System;
using System.Collections.Generic;

#nullable disable

namespace Entity.Models
{
    public partial class City
    {
        public City()
        {
            Districts = new HashSet<District>();
            PrayerTimes = new HashSet<PrayerTime>();
            Subscriptions = new HashSet<Subscription>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }

        public virtual ICollection<District> Districts { get; set; }
        public virtual ICollection<PrayerTime> PrayerTimes { get; set; }
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}

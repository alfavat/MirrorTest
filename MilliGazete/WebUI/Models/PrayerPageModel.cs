using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class PrayerPageModel
    {
        public List<PrayerTimeDto> PrayerTime { get; set; }
        public PageDto Page { get; set; }
    }
}

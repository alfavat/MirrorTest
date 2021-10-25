using AutoMapper;
using Entity.Dtos;
using Entity.Models;
using System;

namespace Business.AutoMapper
{
    public class PrayerTimeMapper : Profile
    {
        public PrayerTimeMapper()
        {
            CreateMap<PrayerTime, PrayerTimeDto>()
                .ForMember(f => f.CityName, g => g.MapFrom(t => t.City.Name));

            CreateMap<PrayerTimeAddDto, PrayerTime>()
                .BeforeMap((dto, entity) => { entity.LastUpdateDate = DateTime.Now; })
                .ForMember(f => f.PrayerDate, g => g.Ignore());
        }
    }
}

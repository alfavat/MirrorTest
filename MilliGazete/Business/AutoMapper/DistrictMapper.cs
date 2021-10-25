using AutoMapper;
using Entity.Dtos;
using Entity.Models;

namespace Business.AutoMapper
{
    public class DistrictMapper : Profile
    {
        public DistrictMapper()
        {
            CreateMap<District, DistrictDto>()
                .ForMember(f => f.CityName, g => g.MapFrom(t => t.City == null ? "" : t.City.Name));
        }
    }
}

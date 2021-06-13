using AutoMapper;
using Entity.Dtos;
using Entity.Models;
using System;

namespace Business.AutoMapper
{
    public class CityMapper : Profile
    {
        public CityMapper()
        {
            CreateMap<City, CityDto>().BeforeMap((dto, entity) => { dto.Districts = null; });
        }
    }
}

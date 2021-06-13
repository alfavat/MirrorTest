using AutoMapper;
using Entity.Dtos;
using Entity.Models;
using System;

namespace Business.AutoMapper
{
    public class DistrictMapper : Profile
    {
        public DistrictMapper()
        {
            CreateMap<District, DistrictDto>().BeforeMap((dto, entity) => { dto.City = null;dto.Subscriptions = null; });
        }
    }
}

using AutoMapper;
using Entity.Dtos;
using Entity.Models;
using System;

namespace Business.AutoMapper
{
    public class AdvertisementMapper : Profile
    {
        public AdvertisementMapper()
        {
            CreateMap<AdvertisementAddDto, Advertisement>().
                BeforeMap((dto, entity) => { entity.CreatedAt = DateTime.Now; entity.Active = true; });

            CreateMap<AdvertisementUpdateDto, Advertisement>();

            CreateMap<Advertisement, AdvertisementDto>();

        }
    }
}

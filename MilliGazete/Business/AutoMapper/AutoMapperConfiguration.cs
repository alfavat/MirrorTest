using AutoMapper;
using Entity.Dtos;
using Entity.Models;
using System;

namespace Business.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<RegisterDto, User>().
                BeforeMap((dto, entity) => { entity.CreatedAt = DateTime.Now; entity.Active = true; });
        }
    }
}

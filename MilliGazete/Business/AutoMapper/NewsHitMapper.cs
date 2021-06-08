using AutoMapper;
using Entity.Dtos;
using Entity.Models;
using System;

namespace Business.AutoMapper
{
    public class NewsHitMapper : Profile
    {
        public NewsHitMapper()
        {
            CreateMap<NewsHitAddDto, NewsHit>().
                BeforeMap((dto, entity) => { entity.CreatedAt = DateTime.Now; });

            CreateMap<NewsHit, NewsHitDto>();

            CreateMap<NewsHitAddDto, NewsHitDetailAddDto>();
        }
    }
}

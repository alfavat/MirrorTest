using AutoMapper;
using Entity.Dtos;
using Entity.Models;
using System;

namespace Business.AutoMapper
{
    public class NewsHitDetailMapper : Profile
    {
        public NewsHitDetailMapper()
        {
            CreateMap<NewsHitDetailAddDto, NewsHitDetail>().
               BeforeMap((dto, entity) => { entity.CreatedAt = DateTime.Now; });

            CreateMap<NewsHitDetail, NewsHitDetailDto>()
                .ForMember(f => f.News, g => g.MapFrom(t => t.News))
                .ForMember(f => f.UserFullName, g => g.MapFrom(u => u.User == null ? "" : u.User.GetFullName()));

        }
    }
}

using AutoMapper;
using Entity.Dtos;
using Entity.Models;
using System;

namespace Business.AutoMapper
{
    public class NewsCommentLikeMapper : Profile
    {
        public NewsCommentLikeMapper()
        {
            CreateMap<NewsCommentLikeAddDto, NewsCommentLike>().
                BeforeMap((dto, entity) => { entity.CreatedAt = DateTime.Now; });

            CreateMap<NewsCommentLikeUpdateDto, NewsCommentLike>();

            CreateMap<NewsCommentLike, NewsCommentLikeDto>()
                .ForMember(f => f.User, u => u.MapFrom(g => g.User == null ? null : g.User));

        }
    }
}

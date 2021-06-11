using AutoMapper;
using Entity.Dtos;
using Entity.Models;
using System;

namespace Business.AutoMapper
{
    public class NewsCommentMapper : Profile
    {
        public NewsCommentMapper()
        {
            CreateMap<NewsCommentAddDto, NewsComment>().
                BeforeMap((dto, entity) => { entity.CreatedAt = DateTime.Now; entity.Approved = false; entity.TotalLikeCount = 0; });

            CreateMap<NewsCommentUpdateDto, NewsComment>();

            CreateMap<NewsComment, NewsCommentDto>()
                .ForMember(f => f.User, u => u.MapFrom(g => g.User == null ? null : g.User))
                .ForMember(f => f.News, u => u.MapFrom(t => t.News));

            CreateMap<NewsComment, UserNewsCommentDto>()
                .ForMember(f => f.User, u => u.MapFrom(g => g.User == null ? null : g.User))
                .ForMember(f => f.NewsCommentLikeList, g => g.MapFrom(t => t.NewsCommentLikes));

            CreateMap<NewsCommentDto, UserNewsCommentDto>()
                .ForMember(f => f.User, u => u.MapFrom(g => g.User == null ? null : g.User));

            CreateMap<News, CommentNewsDetailsDto>()
             .ForMember(f => f.NewsId, u => u.MapFrom(g => g.Id))
             .ForMember(f => f.Url, u => u.MapFrom(g => g.GetUrl()));

        }
    }
}

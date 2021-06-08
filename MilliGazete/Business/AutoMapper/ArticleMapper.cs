using AutoMapper;
using Entity.Dtos;
using Entity.Models;
using System;

namespace Business.AutoMapper
{
    public class ArticleMapper : Profile
    {
        public ArticleMapper()
        {
            CreateMap<ArticleAddDto, Article>().
                BeforeMap((dto, entity) => { entity.CreatedAt = DateTime.Now; entity.Approved = false; })
                .ForMember(f => f.Url, g => g.MapFrom(u => u.Url.ReplaceSpecialCharacters()));

            CreateMap<ArticleUpdateDto, Article>()
                .ForMember(f => f.Url, g => g.MapFrom(u => u.Url.ReplaceSpecialCharacters()));

            CreateMap<Article, ArticleDto>().ForMember(f => f.Author,  u => u.MapFrom(g => g.Author));

        }
    }
}

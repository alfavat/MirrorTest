using AutoMapper;
using Entity.Dtos;
using Entity.Models;
using System;
using System.Linq;

namespace Business.AutoMapper
{
    public class AuthorMapper : Profile
    {
        public AuthorMapper()
        {
            CreateMap<AuthorAddDto, Author>().
                BeforeMap((dto, entity) => { entity.CreatedAt = DateTime.Now; })
                .ForMember(f => f.Url, g => g.MapFrom(u => u.Url.ReplaceSpecialCharacters()))
                .ForMember(f => f.Url, g => g.MapFrom(u => u.Url.ReplaceSpecialCharacters()));

            CreateMap<AuthorUpdateDto, Author>()
                .ForMember(f => f.Url, g => g.MapFrom(u => u.Url.ReplaceSpecialCharacters()))
                .ForMember(f => f.Url, g => g.MapFrom(u => u.Url.ReplaceSpecialCharacters()));

            CreateMap<Author, AuthorDto>()
                .ForMember(f => f.PhotoFile, u => u.MapFrom(g => g.PhotoFile == null ? null : g.PhotoFile))
                .ForMember(f => f.FeaturedImageFile, u => u.MapFrom(g => g.FeaturedImageFile == null ? null : g.FeaturedImageFile));
            //.ForMember(f => f.LastArticleDate, g => g.MapFrom(u => u.Article == null || !u.Article.Any(f => !f.Deleted) ? (DateTime?)null : u.Article.First(f => !f.Deleted).CreatedAt))
            //.ForMember(f => f.LastArticleTitle, g => g.MapFrom(u => u.Article == null || !u.Article.Any(f => !f.Deleted) ? "" : u.Article.First(f => !f.Deleted).Title))
            //.ForMember(f => f.LastArticleUrl, g => g.MapFrom(u => u.Article == null || !u.Article.Any(f => !f.Deleted) ? "" : u.Article.First(f => !f.Deleted).Url));

            CreateMap<Author, AuthorWithDetailsDto>()
               .ForMember(f => f.PhotoFile, u => u.MapFrom(g => g.PhotoFile == null ? null : g.PhotoFile))
               .ForMember(f => f.FeaturedImageFile, u => u.MapFrom(g => g.FeaturedImageFile == null ? null : g.FeaturedImageFile));
               //.ForMember(f => f.ArticleList, g => g.MapFrom(u => u.Article == null || !u.Article.Any(f => !f.Deleted) ? null : u.Article.Where(f => !f.Deleted).ToList()));
        }
    }
}

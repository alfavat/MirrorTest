using AutoMapper;
using Entity.Dtos;
using Entity.Models;
using System;

namespace Business.AutoMapper
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateMap<CategoryAddDto, Category>().
                BeforeMap((dto, entity) => { entity.CreatedAt = DateTime.Now; entity.Active = true; entity.IsStatic = false; })
                .ForMember(f => f.Url, g => g.MapFrom(u => u.Url.ReplaceSpecialCharacters()));

            CreateMap<CategoryUpdateDto, Category>()
                .ForMember(f => f.Url, g => g.MapFrom(u => u.Url.ReplaceSpecialCharacters()));

            CreateMap<Category, CategoryDto>()
                .ForMember(f => f.FeaturedImageFile, g => g.MapFrom(t => t.FeaturedImageFile == null ? null : t.FeaturedImageFile))
                .ForMember(f => f.ParentCategoryName,
                u => u.MapFrom(g => g.ParentCategory == null ? "" : g.ParentCategory.CategoryName));

        }
    }
}

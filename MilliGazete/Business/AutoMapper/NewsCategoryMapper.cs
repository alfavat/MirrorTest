using AutoMapper;
using Entity.Dtos;
using Entity.Models;

namespace Business.AutoMapper
{
    public class NewsCategoryMapper : Profile
    {
        public NewsCategoryMapper()
        {
            CreateMap<NewsCategory, NewsCategoryAddDto>().ReverseMap();
            CreateMap<NewsCategory, NewsCategoryUpdateDto>().ReverseMap();
            CreateMap<NewsCategory, NewsCategoryDto>().ReverseMap();
            CreateMap<NewsCategory, CategoryDto>().ReverseMap();

        }
    }
}

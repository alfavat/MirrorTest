using AutoMapper;
using Entity.Dtos;
using Entity.Models;

namespace Business.AutoMapper
{
    public class NewsPropertyMapper : Profile
    {
        public NewsPropertyMapper()
        {
            CreateMap<NewsProperty, NewsPropertyAddDto>().ReverseMap();
            CreateMap<NewsProperty, NewsPropertyUpdateDto>().ReverseMap();
            CreateMap<NewsProperty, NewsPropertyDto>().ReverseMap();
        }
    }
}

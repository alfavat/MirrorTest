using AutoMapper;
using Entity.Dtos;
using Entity.Models;

namespace Business.AutoMapper
{
    public class NewsFileMapper : Profile
    {
        public NewsFileMapper()
        {
            CreateMap<NewsFile, NewsFileAddDto>().ReverseMap();
            CreateMap<NewsFile, NewsFileUpdateDto>().ReverseMap();

            CreateMap<NewsFile, NewsFileDto>()
                .ForMember(f => f.FileName, g => g.MapFrom(t => t.File == null ? "" : t.File.GetFullFilePath()))
                .ForMember(f => f.CoverFileName, g => g.MapFrom(t => t.VideoCoverFile == null ? "" : t.VideoCoverFile.GetFullFilePath()))
                .ReverseMap();

            CreateMap<NewsFile, MainPageNewsFileDto>()
                .ForMember(f => f.FileName, g => g.MapFrom(t => t.File == null ? "" : t.File.GetFullFilePath()))
                .ForMember(f => f.CoverFileName, g => g.MapFrom(t => t.VideoCoverFile == null ? "" : t.VideoCoverFile.GetFullFilePath()))
                .ReverseMap();

        }
    }
}

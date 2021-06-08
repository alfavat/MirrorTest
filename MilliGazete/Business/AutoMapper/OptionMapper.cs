using AutoMapper;
using Entity.Dtos;
using Entity.Models;

namespace Business.AutoMapper
{
    public class OptionMapper : Profile
    {
        public OptionMapper()
        {
            CreateMap<Option, OptionDto>().ReverseMap();
            CreateMap<Option, OptionAddDto>().ReverseMap();
            CreateMap<OptionUpdateDto, Option>().ReverseMap();
        }
    }
}

using AutoMapper;
using Entity.Dtos;
using Entity.Models;

namespace Business.AutoMapper
{
    public class CurrencyMapper : Profile
    {
        public CurrencyMapper()
        {
            CreateMap<CurrencyAddDto, Currency>();
            CreateMap<Currency, CurrencyDto>().ReverseMap();
        }
    }
}

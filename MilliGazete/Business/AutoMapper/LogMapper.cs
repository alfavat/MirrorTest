using AutoMapper;
using Entity.Dtos;
using Entity.Models;

namespace Business.AutoMapper
{
    public class LogMapper : Profile
    {
        public LogMapper()
        {
            CreateMap<LogDto, Log>();
            CreateMap<Log, LogDto>().ForMember(f => f.UserName, u => u.MapFrom(g => g.User.UserName));
        }
    }
}

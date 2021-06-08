using AutoMapper;
using Entity.Dtos;
using Entity.Models;
using System.Linq;

namespace Business.AutoMapper
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, RegisterDto>();
            CreateMap<UserUpdateDto, User>().ReverseMap();

            CreateMap<UserAddDto, User>().ReverseMap();
            CreateMap<User, UserViewDto>()
            .ForMember(f => f.FirstClaim, u => u.MapFrom(g => !g.UserOperationClaim.Any() ? "" : g.UserOperationClaim.First().OperationClaim.ClaimName));
            CreateMap<UserViewDto, User>();
            CreateMap<CurrentUserUpdateDto, User>().ReverseMap();
        }
    }
}

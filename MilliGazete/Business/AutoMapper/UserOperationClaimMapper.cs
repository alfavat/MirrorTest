using AutoMapper;
using Entity.Dtos;
using Entity.Models;

namespace Business.AutoMapper
{
    public class UserOperationClaimMapper : Profile
    {
        public UserOperationClaimMapper()
        {
            CreateMap<UserOperationClaim, UserOperationClaimViewDto>()
              .ForMember(f => f.OperationClaimName, g => g.MapFrom(f => f.OperationClaim == null ? "" : f.OperationClaim.ClaimName))
              .ForMember(f => f.UserName, g => g.MapFrom(t => t.User == null ? "" : t.User.UserName));
            CreateMap<UserOperationClaimViewDto, UserOperationClaim>();
            CreateMap<UserOperationClaim, UserOperationClaimUpdateDto>().ReverseMap();
        }
    }
}

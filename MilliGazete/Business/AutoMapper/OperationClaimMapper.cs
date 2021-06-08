using AutoMapper;
using Business.Helpers.Concrete;
using Entity.Dtos;
using Entity.Models;

namespace Business.AutoMapper
{
    public class OperationClaimMapper : Profile
    {
        public OperationClaimMapper()
        {
            CreateMap<OperationClaim, OperationClaimDto>().ForMember(f => f.ClaimName, g => g.MapFrom(t => Translator.GetByKey("authorization"+t.ClaimName)));
        }
    }
}

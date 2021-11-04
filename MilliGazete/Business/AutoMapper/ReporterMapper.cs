using AutoMapper;
using Entity.Dtos;
using Entity.Models;

namespace Business.AutoMapper
{
    public class ReporterMapper : Profile
    {
        public ReporterMapper()
        {
            CreateMap<ReporterAddDto, Reporter>().
                BeforeMap((dto, entity) => { entity.Deleted = false; });

            CreateMap<ReporterUpdateDto, Reporter>();

            CreateMap<Reporter, ReporterDto>()
                .ForMember(f => f.ProfileImage, g => g.MapFrom(t => t.ProfileImage == null ? null : t.ProfileImage));

        }
    }
}

using AutoMapper;
using Entity.Dtos;
using Entity.Models;

namespace Business.AutoMapper
{
    public class FileMapper : Profile
    {
        public FileMapper()
        {
            CreateMap<File, FileDto>().ForMember(f => f.FileName, g => g.MapFrom(u => u.FileName.GetFullFilePath()));
            CreateMap<FileDto, File>();
        }
    }
}

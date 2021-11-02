using AutoMapper;
using Entity.Dtos;
using Entity.Models;
using System;

namespace Business.AutoMapper
{
    public class NewspaperMapper : Profile
    {
        public NewspaperMapper()
        {
            CreateMap<NewspaperAddDto, Newspaper>().
                BeforeMap((dto, entity) => { entity.CreatedAt = DateTime.Now; entity.Active = true; });

            CreateMap<Newspaper, NewspaperDto>()
                .ForMember(f => f.MainImage, u => u.MapFrom(g => g.MainImageFileId == null ? null : g.MainImageFile.FileName.GetFullFilePath()))
                .ForMember(f => f.Thumbnail, u => u.MapFrom(g => g.ThumbnailFileId == null ? null : g.ThumbnailFile.FileName.GetFullFilePath()));
        }
    }
}

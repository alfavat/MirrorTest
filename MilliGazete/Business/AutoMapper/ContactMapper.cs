using AutoMapper;
using Entity.Dtos;
using Entity.Models;
using System;

namespace Business.AutoMapper
{
    public class ContactMapper : Profile
    {
        public ContactMapper()
        {
            CreateMap<ContactAddDto, Contact>().BeforeMap((dto, entity) => { entity.CreatedAt = DateTime.Now; });

            CreateMap<Contact, ContactDto>();

        }
    }
}

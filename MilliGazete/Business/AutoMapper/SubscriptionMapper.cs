using AutoMapper;
using Entity.Dtos;
using Entity.Models;
using System;

namespace Business.AutoMapper
{
    public class SubscriptionMapper : Profile
    {
        public SubscriptionMapper()
        {
            CreateMap<SubscriptionAddDto, Subscription>().BeforeMap((dto, entity) => { entity.CreatedAt = DateTime.Now; });

            CreateMap<Subscription, SubscriptionDto>()
                .ForMember(f => f.District, g => g.MapFrom(t => t.District == null ? null : t.District));

        }
    }
}

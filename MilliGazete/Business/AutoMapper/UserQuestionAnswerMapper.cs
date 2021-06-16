using AutoMapper;
using Entity.Dtos;
using Entity.Models;
using System;

namespace Business.AutoMapper
{
    public class UserQuestionAnswerMapper : Profile
    {
        public UserQuestionAnswerMapper()
        {
            CreateMap<UserQuestionAnswerAddDto, UserQuestionAnswer>().
                BeforeMap((dto, entity) => { entity.CreatedAt = DateTime.Now; });

            CreateMap<UserQuestionAnswer, UserQuestionAnswerDto>()
                .ForMember(f => f.Answer, g => g.MapFrom(t => t.Answer == null ? null : t.Answer))
                .ForMember(f => f.Question, g => g.MapFrom(t => t.Question == null ? null : t.Question));

        }
    }
}

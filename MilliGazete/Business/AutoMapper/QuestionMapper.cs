using AutoMapper;
using Entity.Dtos;
using Entity.Models;
using System;

namespace Business.AutoMapper
{
    public class QuestionMapper : Profile
    {
        public QuestionMapper()
        {
            CreateMap<QuestionAddDto, Question>().
                BeforeMap((dto, entity) => { entity.CreatedAt = DateTime.Now; });

            CreateMap<QuestionUpdateDto, Question>();

            CreateMap<Question, QuestionDto>()
                .ForMember(f => f.QuestionAnswers, g => g.MapFrom(t => t.QuestionAnswers == null ? null : t.QuestionAnswers));

        }
    }
}

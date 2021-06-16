using AutoMapper;
using Entity.Dtos;
using Entity.Models;
using System;

namespace Business.AutoMapper
{
    public class QuestionAnswerMapper : Profile
    {
        public QuestionAnswerMapper()
        {
            CreateMap<QuestionAnswerAddDto, QuestionAnswer>();

            CreateMap<QuestionAnswerUpdateDto, QuestionAnswer>();

            CreateMap<QuestionAnswer, QuestionAnswerDto>();

        }
    }
}

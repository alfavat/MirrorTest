﻿using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IQuestionService
    {
        Task<IDataResult<QuestionDto>> GetById(int questionId);
        Task<IResult> Update(QuestionUpdateDto questionUpdateDto);
        Task<IDataResult<int>> Add(QuestionAddDto dto);
        Task<IResult> Delete(int questionId);
        Task<IDataResult<List<QuestionDto>>> GetList();
    }
}

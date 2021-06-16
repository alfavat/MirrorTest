using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : MainController
    {
        private IQuestionService _questionService;
        public QuestionsController(IQuestionService QuestionService)
        {
            _questionService = QuestionService;
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList()
        {
            return GetResponse(await _questionService.GetList());
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int questionId)
        {
            return GetResponse(await _questionService.GetById(questionId));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(QuestionAddDto questionAddDto)
        {
            return GetResponse(await _questionService.Add(questionAddDto));
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(QuestionUpdateDto questionUpdateDto)
        {
            return GetResponse(await _questionService.Update(questionUpdateDto));
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int questionId)
        {
            return GetResponse(await _questionService.Delete(questionId));
        }
    }
}
using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionAnswerAnswersController : MainController
    {
        private IQuestionAnswerService _questionAnswerService;
        public QuestionAnswerAnswersController(IQuestionAnswerService QuestionAnswerService)
        {
            _questionAnswerService = QuestionAnswerService;
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList()
        {
            return GetResponse(await _questionAnswerService.GetList());
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int questionAnswerId)
        {
            return GetResponse(await _questionAnswerService.GetById(questionAnswerId));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(QuestionAnswerAddDto questionAnswerAddDto)
        {
            return GetResponse(await _questionAnswerService.Add(questionAnswerAddDto));
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(QuestionAnswerUpdateDto questionAnswerUpdateDto)
        {
            return GetResponse(await _questionAnswerService.Update(questionAnswerUpdateDto));
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int questionAnswerId)
        {
            return GetResponse(await _questionAnswerService.Delete(questionAnswerId));
        }
    }
}
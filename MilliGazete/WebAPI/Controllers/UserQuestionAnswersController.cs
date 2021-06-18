using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserQuestionAnswersController : MainController
    {
        private IUserQuestionAnswerService _userQuestionAnswerService;
        public UserQuestionAnswersController(IUserQuestionAnswerService UserQuestionAnswerService)
        {
            _userQuestionAnswerService = UserQuestionAnswerService;
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList()
        {
            return GetResponse(await _userQuestionAnswerService.GetList());
        }

        [HttpGet("getisanswered")]
        public async Task<IActionResult> GetIsAnswered(int questionId)
        {
            return GetResponse(await _userQuestionAnswerService.GetIsAnswered(questionId));
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int userQuestionAnswerId)
        {
            return GetResponse(await _userQuestionAnswerService.GetById(userQuestionAnswerId));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(UserQuestionAnswerAddDto userQuestionAnswerAddDto)
        {
            return GetResponse(await _userQuestionAnswerService.Add(userQuestionAnswerAddDto));
        }
    }
}
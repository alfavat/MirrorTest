using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : MainController
    {
        private IAuthorService _authorService;
        public AuthorsController(IAuthorService AuthorService)
        {
            _authorService = AuthorService;
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList()
        {
            return GetResponse(await _authorService.GetList());
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int authorId)
        {
            return GetResponse(await _authorService.GetById(authorId));
        }

        [HttpGet("getbyname")]
        public async Task<IActionResult> GetByName(string nameSurename)
        {
            return GetResponse(await _authorService.GetByName(nameSurename));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(AuthorAddDto dto)
        {
            return GetResponse(await _authorService.Add(dto));
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(AuthorUpdateDto dto)
        {
            return GetResponse(await _authorService.Update(dto));
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int authorId)
        {
            return GetResponse(await _authorService.Delete(authorId));
        }
    }
}
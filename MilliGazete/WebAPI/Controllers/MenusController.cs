using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : MainController
    {
        private IMenuService _menuService;
        public MenusController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList()
        {
            return GetResponse(await _menuService.GetList());
        }

        [HttpGet("getactivelist")]
        public async Task<IActionResult> GetActiveLis()
        {
            return GetResponse(await _menuService.GetActiveList());
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            return GetResponse(await _menuService.GetById(id));
        }

        [HttpPost("changeactivestatus")]
        public async Task<IActionResult> ChangeActiveStatus(ChangeActiveStatusDto changeActiveStatusDto)
        {
            return GetResponse(await _menuService.ChangeActiveStatus(changeActiveStatusDto));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(MenuAddDto dto)
        {
            return GetResponse(await _menuService.Add(dto));
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(MenuUpdateDto dto)
        {
            return GetResponse(await _menuService.Update(dto));
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            return GetResponse(await _menuService.Delete(id));
        }
    }
}
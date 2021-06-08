using Business.Managers.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntitiesController : MainController
    {
        private readonly IEntityService _entityService;
        public EntitiesController(IEntityService entityService)
        {
            _entityService = entityService;
        }

        [HttpGet("getcounterentities")]
        public async Task<IActionResult> GetCounterEntities()
        {
            return GetResponse(await _entityService.GetCounterEntities());
        }

        [HttpGet("getnewstypeentities")]
        public async Task<IActionResult> GetNewsTypeEntities()
        {
            return GetResponse(await _entityService.GetNewsTypeEntities());
        }

        [HttpGet("getnewsagencyentities")]
        public async Task<IActionResult> GetNewsAgencyEntities()
        {
            return GetResponse(await _entityService.GetNewsAgencyEntities());
        }

        [HttpGet("getnewsfiletypeentities")]
        public async Task<IActionResult> GetNewsFileTypeEntities()
        {
            return GetResponse(await _entityService.GetNewsFileTypeEntities());
        }

        [HttpGet("getpropertyentities")]
        public async Task<IActionResult> GetPropertyEntities()
        {
            return GetResponse(await _entityService.GetPropertyEntities());
        }

        [HttpGet("getpositionentities")]
        public async Task<IActionResult> GetPositionEntities()
        {
            return GetResponse(await _entityService.GetPositionEntities());
        }
    }
}
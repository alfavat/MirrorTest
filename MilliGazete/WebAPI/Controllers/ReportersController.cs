using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportersController : MainController
    {
        private IReporterService _reporterService;
        public ReportersController(IReporterService ReporterService)
        {
            _reporterService = ReporterService;
        }

        [HttpGet("getnewslistbyurl")]
        public async Task<IActionResult> GetNewsListByUrl(string url, string query, int limit = 10, string orderBy = "Id", int page = 1, int ascending = 2)
        {
            var result = await _reporterService.GetListByPagingViaUrl(new ReporterNewsPagingDto()
            {
                Query = query,
                Limit = limit,
                OrderBy = orderBy + (ascending == 1 ? " ascending" : " descending"),
                PageNumber = page,
                Url = url
            });
            if (result.Success)
                return Ok(new { data = new { data = result.Data.Item1, count = result.Data.Item2 }, result.Message, result.Success });
            return BadRequest(result);
        }

        [HttpGet("getlist")]
        public async Task<IActionResult> GetList()
        {
            return GetResponse(await _reporterService.GetList());
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int reporterId)
        {
            return GetResponse(await _reporterService.GetById(reporterId));
        }

        [HttpGet("getbyurl")]
        public async Task<IActionResult> GetByUrl(string url)
        {
            return GetResponse(await _reporterService.GetByUrl(url));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(ReporterAddDto reporterAddDto)
        {
            return GetResponse(await _reporterService.Add(reporterAddDto));
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(ReporterUpdateDto reporterUpdateDto)
        {
            return GetResponse(await _reporterService.Update(reporterUpdateDto));
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(int reporterId)
        {
            return GetResponse(await _reporterService.Delete(reporterId));
        }
    }
}
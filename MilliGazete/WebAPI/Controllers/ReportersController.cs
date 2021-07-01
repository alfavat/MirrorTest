using Business.Managers.Abstract;
using Entity.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportersController : MainController
    {
        private IReporterService _reporterService;
        private INewsService _newsService;
        public ReportersController(IReporterService ReporterService, INewsService newsService)
        {
            _reporterService = ReporterService;
            _newsService = newsService;
        }

        [HttpGet("getbyreporterid")]
        public async Task<IActionResult> GetListByreporterId(int reporterId)
        {
            return GetResponse(await _newsService.GetListByReporterId(reporterId));
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
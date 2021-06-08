using Business.Managers.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardsController : MainController
    {
        private readonly IDashboardService _dashboardService;
        public DashboardsController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("getdashboardstatistics")]
        public async Task<IActionResult> GetDashboardStatistics()
        {
            return GetResponse(await _dashboardService.GetDashboardStatistics());
        }

        [HttpGet("getdashboardtopcommentnews")]
        public async Task<IActionResult> GetDashboardTopCommentNews(int limit = 10)
        {
            return GetResponse(await _dashboardService.GetDashboardTopCommentNews(limit));
        }

        [HttpGet("getdashboardchartdata")]
        public async Task<IActionResult> GetDashboardChartData(DateTime fromDate)
        {
            return GetResponse(await _dashboardService.GetDashboardChartData(fromDate));
        }
    }
}
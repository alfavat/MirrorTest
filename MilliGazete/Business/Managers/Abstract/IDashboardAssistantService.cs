using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IDashboardAssistantService
    {
        Task<DashboardStatisticsDto> GetDashboardStatistics();
        Task<List<DashboardTopCommentNewsDto>> GetDashboardTopCommentNews(int limit);
        Task<List<DashboardChartDataDto>> GetDashboardChartData(DateTime fromDate);
    }
}

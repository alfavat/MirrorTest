using Core.Utilities.Results;
using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IDashboardService
    {
        Task<IDataResult<DashboardStatisticsDto>> GetDashboardStatistics();
        Task<IDataResult<List<DashboardTopCommentNewsDto>>> GetDashboardTopCommentNews(int limit);
        Task<IDataResult<List<DashboardChartDataDto>>> GetDashboardChartData(DateTime fromDate);
    }
}

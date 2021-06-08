using Business.Managers.Abstract;
using DataAccess.Abstract;
using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class DashboardAssistantManager : IDashboardAssistantService
    {
        private readonly INewsDal _newsDal;
        public DashboardAssistantManager(INewsDal newsDal)
        {
            _newsDal = newsDal;
        }

        public async Task<List<DashboardChartDataDto>> GetDashboardChartData(DateTime fromDate)
        {
            return await _newsDal.GetDashboardChartData(fromDate);
        }

        public async Task<DashboardStatisticsDto> GetDashboardStatistics()
        {
            return await _newsDal.GetDashboardStatistics();
        }

        public async Task<List<DashboardTopCommentNewsDto>> GetDashboardTopCommentNews(int limit)
        {
            return await _newsDal.GetDashboardTopCommentNews(limit);
        }
    }
}

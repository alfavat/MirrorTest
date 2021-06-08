using Business.BusinessAspects.Autofac;
using Business.Managers.Abstract;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class DashboardManager : IDashboardService
    {
        private readonly IDashboardAssistantService _dashboardAssistantService;
        public DashboardManager(IDashboardAssistantService dashboardAssistant)
        {
            _dashboardAssistantService = dashboardAssistant;
        }

        [SecuredOperation("DashboardGet")]
        [PerformanceAspect()]
        public async Task<IDataResult<DashboardStatisticsDto>> GetDashboardStatistics()
        {
            return new SuccessDataResult<DashboardStatisticsDto>(await _dashboardAssistantService.GetDashboardStatistics());
        }

        [SecuredOperation("DashboardGet")]
        [PerformanceAspect()]
        public async Task<IDataResult<List<DashboardTopCommentNewsDto>>> GetDashboardTopCommentNews(int limit)
        {
            return new SuccessDataResult<List<DashboardTopCommentNewsDto>>(await _dashboardAssistantService.GetDashboardTopCommentNews(limit));
        }

        [SecuredOperation("DashboardGet")]
        [PerformanceAspect()]
        public async Task<IDataResult<List<DashboardChartDataDto>>> GetDashboardChartData(DateTime fromDate)
        {
            return new SuccessDataResult<List<DashboardChartDataDto>>(await _dashboardAssistantService.GetDashboardChartData(fromDate));
        }
    }
}

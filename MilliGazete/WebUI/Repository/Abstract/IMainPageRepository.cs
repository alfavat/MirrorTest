using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Repository.Abstract
{
    public interface IMainPageRepository : IUIBaseRepository
    {
        Task<IDataResult<List<FlashNewsDto>>> GetLastFlashNews(int limit = 0);
        Task<IDataResult<List<MenuViewDto>>> GetMenuList();
        Task<IDataResult<List<MainHeadingDto>>> GetMainHeadingNews(int limit = 0);
        Task<IDataResult<List<SubHeadingDto>>> GetSubHeadingNews(int limit = 0);
        Task<IDataResult<List<SubHeadingDto>>> GetSubHeadingNews2(int limit = 0);
        Task<IDataResult<List<SuperLeagueStandingsDto>>> GetSuperLeagueStandings();
        Task<IDataResult<List<BreakingNewsDto>>> GetTopBreakingNews(int limit = 0);
        Task<IDataResult<List<MainPageVideoNewsDto>>> GetTopVideoNews(int limit = 0);
        Task<IDataResult<List<MainPageImageNewsDto>>> GetTopImageNews(int limit = 0);
        Task<IDataResult<List<MainPageNewsDto>>> GetTopMainPageNews(int limit = 0);
        Task<IDataResult<List<MainPageFourHillNewsDto>>> GetMainPageFourHillNews(int limit = 0);
        Task<IDataResult<List<MainPageHeadlineSideNewsDto>>> GetMainPageSideNews(int limit = 0);
        Task<IDataResult<List<WeatherInfoDto>>> GetWeatherInfo();
        Task<IDataResult<List<CurrencyDto>>> GetCurrencyList();
        Task<IDataResult<List<LiveNarrotationNewsDto>>> GetLiveNarrotationNewsList(int limit = 0);
        Task<IDataResult<List<WideHeadingNewsDto>>> GetTopWideHeadingNews(int limit = 0);
    }
}

using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using WebMobileUI.Models;
namespace WebMobileUI.Repository.Abstract
{
    public interface IMainPageRepository : IUIBaseRepository
    {
        IDataResult<List<MenuDto>> GetMenuList();
        IDataResult<List<CustomHeading>> GetMainHeadingNews(int limit = 0);
        IDataResult<List<CustomHeading>> GetSubHeadingNews(int limit = 0);
        IDataResult<List<SuperLeagueStandingsDto>> GetSuperLeagueStandings();
        IDataResult<List<BreakingNewsDto>> GetTopBreakingNews(int limit = 0);
        IDataResult<List<MainPageVideoNewsDto>> GetTopVideoNews(int limit = 0);
        IDataResult<List<MainPageImageNewsDto>> GetTopImageNews(int limit = 0);
        IDataResult<List<MainPageNewsDto>> GetTopMainPageNews(int limit = 0);
        IDataResult<List<WeatherInfoDto>> GetWeatherInfo();
        IDataResult<List<CurrencyDto>> GetCurrencyList();
        IDataResult<List<MainPageStoryNewsDto>> GetStoryNewsList(int limit = 0);
    }
}

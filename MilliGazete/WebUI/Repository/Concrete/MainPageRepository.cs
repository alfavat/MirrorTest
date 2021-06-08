using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Repository.Abstract;

namespace WebUI.Repository.Concrete
{
    public class MainPageRepository : IMainPageRepository
    {
        public async Task<IDataResult<List<CurrencyDto>>> GetCurrencyList()
        {
            return await ApiHelper.GetApiAsync<List<CurrencyDto>>("Currencies/getlist");
        }

        public async Task<IDataResult<List<MenuViewDto>>> GetMenuList()
        {
            return await ApiHelper.GetApiAsync<List<MenuViewDto>>("Menus/getactivelist");
        }

        public async Task<IDataResult<List<MainHeadingDto>>> GetMainHeadingNews(int limit = 0)
        {
            string param = "";
            if (limit != 0) param = "?limit=" + limit;
            return await ApiHelper.GetApiAsync<List<MainHeadingDto>>("MainPage/getmainheadingnews" + param);
        }
        public async Task<IDataResult<List<SubHeadingDto>>> GetSubHeadingNews(int limit = 0)
        {
            string param = "";
            if (limit != 0) param = "?limit=" + limit;
            return await ApiHelper.GetApiAsync<List<SubHeadingDto>>("MainPage/getsubheadingnews" + param);
        }

        public async Task<IDataResult<List<SubHeadingDto>>> GetSubHeadingNews2(int limit = 0)
        {
            string param = "";
            if (limit != 0) param = "?limit=" + limit;
            return await ApiHelper.GetApiAsync<List<SubHeadingDto>>("MainPage/getsubheadingnews2" + param);
        }

        public async Task<IDataResult<List<SuperLeagueStandingsDto>>> GetSuperLeagueStandings()
        {
            return await ApiHelper.GetApiAsync<List<SuperLeagueStandingsDto>>("MainPage/getsuperleaguestandings");
        }

        public async Task<IDataResult<List<BreakingNewsDto>>> GetTopBreakingNews(int limit = 0)
        {
            string param = "";
            if (limit != 0) param = "?limit=" + limit;
            return await ApiHelper.GetApiAsync<List<BreakingNewsDto>>("MainPage/gettopbreakingnews" + param);
        }

        public async Task<IDataResult<List<MainPageVideoNewsDto>>> GetTopVideoNews(int limit = 0)
        {
            string param = "";
            if (limit != 0) param = "?limit=" + limit;
            return await ApiHelper.GetApiAsync<List<MainPageVideoNewsDto>>("MainPage/gettopvideonews" + param);
        }

        public async Task<IDataResult<List<MainPageImageNewsDto>>> GetTopImageNews(int limit = 0)
        {
            string param = "";
            if (limit != 0) param = "?limit=" + limit;
            return await ApiHelper.GetApiAsync<List<MainPageImageNewsDto>>("MainPage/gettopimagenews" + param);
        }

        public async Task<IDataResult<List<MainPageNewsDto>>> GetTopMainPageNews(int limit = 0)
        {
            string param = "";
            if (limit != 0) param = "?limit=" + limit;
            return await ApiHelper.GetApiAsync<List<MainPageNewsDto>>("MainPage/gettopmainpagenews" + param);
        }

        public async Task<IDataResult<List<MainPageFourHillNewsDto>>> GetMainPageFourHillNews(int limit = 0)
        {
            string param = "";
            if (limit != 0) param = "?limit=" + limit;
            return await ApiHelper.GetApiAsync<List<MainPageFourHillNewsDto>>("MainPage/gettopmainpagefourhillnews" + param);
        }

        public async Task<IDataResult<List<MainPageHeadlineSideNewsDto>>> GetMainPageSideNews(int limit = 0)
        {
            string param = "";
            if (limit != 0) param = "?limit=" + limit;
            return await ApiHelper.GetApiAsync<List<MainPageHeadlineSideNewsDto>>("MainPage/gettopmainpageheadlinesidenews" + param);
        }

        public async Task<IDataResult<List<WeatherInfoDto>>> GetWeatherInfo()
        {
            return await ApiHelper.GetApiAsync<List<WeatherInfoDto>>("MainPage/getweatherinfo");
        }

        public async Task<IDataResult<List<LiveNarrotationNewsDto>>> GetLiveNarrotationNewsList(int limit = 0)
        {
            string param = "";
            if (limit != 0) param = "?limit=" + limit;
            return await ApiHelper.GetApiAsync<List<LiveNarrotationNewsDto>>("MainPage/getlivenarrotationnews" + param);
        }

        public async Task<IDataResult<List<WideHeadingNewsDto>>> GetTopWideHeadingNews(int limit = 0)
        {
            string param = "";
            if (limit != 0) param = "?limit=" + limit;
            return await ApiHelper.GetApiAsync<List<WideHeadingNewsDto>>("MainPage/getwideheadingnews" + param);
        }
    }
}

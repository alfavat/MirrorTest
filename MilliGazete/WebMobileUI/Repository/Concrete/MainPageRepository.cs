using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using WebMobileUI.Models;
using WebMobileUI.Repository.Abstract;

namespace WebMobileUI.Repository.Concrete
{
    public class MainPageRepository : IMainPageRepository
    {
        public IDataResult<List<CurrencyDto>> GetCurrencyList()
        {
            return ApiHelper.GetApi<List<CurrencyDto>>("Currencies/getlist");
        }

        public IDataResult<List<MenuDto>> GetMenuList()
        {
            return ApiHelper.GetApi<List<MenuDto>>("MainPage/getmainpagemenulist");
        }

        public IDataResult<List<CustomHeading>> GetMainHeadingNews(int limit = 0)
        {
            string param = "";
            if (limit != 0) param = "?limit=" + limit;
            return ApiHelper.GetApi<List<CustomHeading>>("MainPage/getmainheadingnews" + param);
        }

        public IDataResult<List<CustomHeading>> GetSubHeadingNews(int limit = 0)
        {
            string param = "";
            if (limit != 0) param = "?limit=" + limit;
            return ApiHelper.GetApi<List<CustomHeading>>("MainPage/getsubheadingnews" + param);
        }

        public IDataResult<List<SuperLeagueStandingsDto>> GetSuperLeagueStandings()
        {
            return ApiHelper.GetApi<List<SuperLeagueStandingsDto>>("MainPage/getsuperleaguestandings");
        }

        public IDataResult<List<BreakingNewsDto>> GetTopBreakingNews(int limit = 0)
        {
            string param = "";
            if (limit != 0) param = "?limit=" + limit;
            return ApiHelper.GetApi<List<BreakingNewsDto>>("MainPage/gettopbreakingnews" + param);
        }

        public IDataResult<List<MainPageVideoNewsDto>> GetTopVideoNews(int limit = 0)
        {
            string param = "";
            if (limit != 0) param = "?limit=" + limit;
            return ApiHelper.GetApi<List<MainPageVideoNewsDto>>("MainPage/gettopvideonews" + param);
        }

        public IDataResult<List<MainPageImageNewsDto>> GetTopImageNews(int limit = 0)
        {
            string param = "";
            if (limit != 0) param = "?limit=" + limit;
            return ApiHelper.GetApi<List<MainPageImageNewsDto>>("MainPage/gettopimagenews" + param);
        }

        public IDataResult<List<MainPageNewsDto>> GetTopMainPageNews(int limit = 0)
        {
            string param = "";
            if (limit != 0) param = "?limit=" + limit;
            return ApiHelper.GetApi<List<MainPageNewsDto>>("MainPage/gettopmainpagenews" + param);
        }

        public IDataResult<List<WeatherInfoDto>> GetWeatherInfo()
        {
            return ApiHelper.GetApi<List<WeatherInfoDto>>("MainPage/getweatherinfo");
        }

        public IDataResult<List<MainPageStoryNewsDto>> GetStoryNewsList(int limit = 0)
        {
            string param = "";
            if (limit != 0) param = "?limit=" + limit;
            return ApiHelper.GetApi<List<MainPageStoryNewsDto>>("MainPage/getstorynews" + param);
        }
    }
}

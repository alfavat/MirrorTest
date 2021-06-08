using Business.Managers.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Managers.Concrete
{
    public class MainPageManager : IMainPageService
    {
        private readonly IMainPageAssistantService _mainPageAssistantService;

        public MainPageManager(IMainPageAssistantService mainPageAssistantService)
        {
            _mainPageAssistantService = mainPageAssistantService;
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public IDataResult<List<MenuDto>> GetMainPageMenuList()
        {
            var list = new List<MenuDto>()
            {
                new MenuDto(){Order=1, MenuName="Gündem", Url="gundem"},
                new MenuDto(){Order=1, MenuName="Ekonomi", Url="ekonomi"},
                new MenuDto(){Order=1, MenuName="Dünya", Url="dunya"},
                new MenuDto(){Order=1, MenuName="Spor", Url="spor"},
                new MenuDto(){Order=1, MenuName="Foto Galeri", Url="foto-galeri"},
                new MenuDto(){Order=1, MenuName="Video", Url="video"},
                new MenuDto(){Order=1, MenuName="Yazarlar", Url="yazarlar"}
            };
            return new SuccessDataResult<List<MenuDto>>(list);
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<BreakingNewsDto>>> GetTopBreakingNews(int limit)
        {
            return new SuccessDataResult<List<BreakingNewsDto>>(await _mainPageAssistantService.GetTopBreakingNews(limit));
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<WideHeadingNewsDto>>> GetTopWideHeadingNews(int limit)
        {
            return new SuccessDataResult<List<WideHeadingNewsDto>>(await _mainPageAssistantService.GetTopWideHeadingNews(limit));
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<SubHeadingDto>>> GetSubHeadingNews(int limit)
        {
            return new SuccessDataResult<List<SubHeadingDto>>(await _mainPageAssistantService.GetTopSubHeadingNews(limit));
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<SubHeadingDto>>> GetSubHeadingNews2(int limit)
        {
            return new SuccessDataResult<List<SubHeadingDto>>(await _mainPageAssistantService.GetTopSubHeadingNews2(limit));
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<MainHeadingDto>>> GetMainHeadingNews(int limit)
        {
            return new SuccessDataResult<List<MainHeadingDto>>(await _mainPageAssistantService.GetTopMainHeadingNews(limit));
        }


        [CacheAspect()]
        [PerformanceAspect()]
        public IDataResult<WeatherInfoDto> GetWeatherInfo()
        {
            var weatherInfoDto = new List<WeatherInfoDto>{
                new WeatherInfoDto()
            {
                CityName = "İstanbul",
                Temperature = 14.7,
                CityImageUrl = "/Resources/Cities/istanbul.png",
                Key = "rainy"
            } };
            return new SuccessDataResult<WeatherInfoDto>(weatherInfoDto.FirstOrDefault());
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public IDataResult<MainPageFixedFieldDto> GetFixedFields()
        {
            var fixedFields = new List<MainPageFixedFieldDto>{
                new MainPageFixedFieldDto()
            {
                LiveVideoLink = "https://www.youtube.com/embed/wd2LOnn19UI"
            }
            };
            return new SuccessDataResult<MainPageFixedFieldDto>(fixedFields.FirstOrDefault());
        }


        [CacheAspect()]
        [PerformanceAspect()]
        public IDataResult<List<StreamingDto>> GetStreamingList()
        {
            var streamingList = new List<StreamingDto>()
            {
                new StreamingDto(){Title= "Ana Haber Bülteni", ImageUrl="/Resources/Cities/istanbul.png", Time="19:00"},
                new StreamingDto(){Title= "Ana Haber Bülteni", ImageUrl="/Resources/Cities/istanbul.png", Time="19:00"},
                new StreamingDto(){Title= "Ana Haber Bülteni", ImageUrl="/Resources/Cities/istanbul.png", Time="19:00"},
                new StreamingDto(){Title= "Ana Haber Bülteni", ImageUrl="/Resources/Cities/istanbul.png", Time="19:00"},
                new StreamingDto(){Title= "Ana Haber Bülteni", ImageUrl="/Resources/Cities/istanbul.png", Time="19:00"},
                new StreamingDto(){Title= "Ana Haber Bülteni", ImageUrl="/Resources/Cities/istanbul.png", Time="19:00"}
            };
            return new SuccessDataResult<List<StreamingDto>>(streamingList);
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<MainPageNewsDto>>> GetTopMainPageNews(int limit)
        {
            return new SuccessDataResult<List<MainPageNewsDto>>(await _mainPageAssistantService.GetTopMainPageNews(limit));
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<MainPageVideoNewsDto>>> GetTopVideoNewsAsync(int limit)
        {
            return new SuccessDataResult<List<MainPageVideoNewsDto>>(await _mainPageAssistantService.GetLastVideoNews(limit));
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<MainPageImageNewsDto>>> GetTopImageNewsAsync(int limit)
        {
            return new SuccessDataResult<List<MainPageImageNewsDto>>(await _mainPageAssistantService.GetLastImageNews(limit));
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public IDataResult<List<SuperLeagueStandingsDto>> GetSuperLeagueStandings()
        {
            var list = new List<SuperLeagueStandingsDto>()
            {
                new SuperLeagueStandingsDto(){Order=1, TeamName="Galatasaray",PlayedMatchCount=7,Point=22 },
                new SuperLeagueStandingsDto(){Order=2, TeamName="Galatasaray",PlayedMatchCount=7,Point=22 },
                new SuperLeagueStandingsDto(){Order=3, TeamName="Galatasaray",PlayedMatchCount=7,Point=22 },
                new SuperLeagueStandingsDto(){Order=4, TeamName="Galatasaray",PlayedMatchCount=7,Point=22 },
                new SuperLeagueStandingsDto(){Order=5, TeamName="Galatasaray",PlayedMatchCount=7,Point=22 },
                new SuperLeagueStandingsDto(){Order=6, TeamName="Galatasaray",PlayedMatchCount=7,Point=22 },
                new SuperLeagueStandingsDto(){Order=7, TeamName="Galatasaray",PlayedMatchCount=7,Point=22 },
                new SuperLeagueStandingsDto(){Order=8, TeamName="Galatasaray",PlayedMatchCount=7,Point=22 },
                new SuperLeagueStandingsDto(){Order=9, TeamName="Galatasaray",PlayedMatchCount=7,Point=22 },
                new SuperLeagueStandingsDto(){Order=10, TeamName="Galatasaray",PlayedMatchCount=7,Point=22 },
                new SuperLeagueStandingsDto(){Order=11, TeamName="Galatasaray",PlayedMatchCount=7,Point=22 },
                new SuperLeagueStandingsDto(){Order=12, TeamName="Galatasaray",PlayedMatchCount=7,Point=22 },
                new SuperLeagueStandingsDto(){Order=13, TeamName="Galatasaray",PlayedMatchCount=7,Point=22 },
                new SuperLeagueStandingsDto(){Order=14, TeamName="Galatasaray",PlayedMatchCount=7,Point=22 },
                new SuperLeagueStandingsDto(){Order=15, TeamName="Galatasaray",PlayedMatchCount=7,Point=22 },
                new SuperLeagueStandingsDto(){Order=16, TeamName="Galatasaray",PlayedMatchCount=7,Point=22 },
                new SuperLeagueStandingsDto(){Order=17, TeamName="Galatasaray",PlayedMatchCount=7,Point=22 },
                new SuperLeagueStandingsDto(){Order=18, TeamName="Galatasaray",PlayedMatchCount=7,Point=22 }
            };
            return new SuccessDataResult<List<SuperLeagueStandingsDto>>(list);
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<MainPageStoryNewsDto>>> GetStoryNewsAsync(int limit)
        {
            return new SuccessDataResult<List<MainPageStoryNewsDto>>(await _mainPageAssistantService.GetStoryNews(limit));
        }

        [CacheAspect()]
        [PerformanceAspect()]
        public async Task<IDataResult<List<MainPageFourHillNewsDto>>> GetTopMainPageFourHillNews(int limit)
        {
            return new SuccessDataResult<List<MainPageFourHillNewsDto>>(await _mainPageAssistantService.GetTopMainPageFourHillNews(limit));
        }
    }
}

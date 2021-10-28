using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Managers.Abstract
{
    public interface IMainPageService
    {
        IDataResult<List<MenuDto>> GetMainPageMenuList();
        Task<IDataResult<List<BreakingNewsDto>>> GetTopBreakingNews(int limit);
        Task<IDataResult<List<SubHeadingDto>>> GetSubHeadingNews(int limit);
        Task<IDataResult<List<SubHeadingDto>>> GetSubHeadingNews2(int limit);
        Task<IDataResult<List<MainHeadingDto>>> GetMainHeadingNews(int limit);
        IDataResult<WeatherInfoDto> GetWeatherInfo();
        IDataResult<MainPageFixedFieldDto> GetFixedFields();
        IDataResult<List<StreamingDto>> GetStreamingList();
        Task<IDataResult<List<FlashNewsDto>>> GetLastFlashNews(int limit);
        Task<IDataResult<List<MainPageNewsDto>>> GetTopMainPageNews(int limit);
        Task<IDataResult<List<MainPageVideoNewsDto>>> GetTopVideoNewsAsync(int limit);
        IDataResult<List<SuperLeagueStandingsDto>> GetSuperLeagueStandings();
        Task<IDataResult<List<MainPageImageNewsDto>>> GetTopImageNewsAsync(int limit);
        Task<IDataResult<List<MainPageStoryNewsDto>>> GetStoryNewsAsync(int limit);
        Task<IDataResult<List<MainPageFourHillNewsDto>>> GetTopMainPageFourHillNews(int limit);
        Task<IDataResult<List<WideHeadingNewsDto>>> GetTopWideHeadingNews(int limit);
    }
}

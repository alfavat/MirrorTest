using Core.Utilities.Results;
using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMobileUI.Models
{
    public class MainPage
    {
        public List<BreakingNewsDto> BreakingNews { get; set; }
        public List<SubHeadingDto> SubHeadings { get; set; }
        public List<MainHeadingDto> MainHeadings { get; set; }
        public MainPageFixedFieldDto MainPageFixedField { get; set; }
        public List<MainPageNewsDto> MainPageNews { get; set; }
        public List<StreamingDto> Streamings { get; set; }
        public List<MainPageVideoNewsDto> MainPageVideoNews { get; set; }
        public WeatherInfoDto WeatherInfo { get; set; }
        public List<SuperLeagueStandingsDto> SuperLeagueStandings { get; set; }
    }
}

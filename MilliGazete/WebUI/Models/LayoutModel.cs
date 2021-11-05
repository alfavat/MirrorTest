using Entity.Dtos;
using Entity.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using WebUI.Repository.Abstract;

namespace WebUI.Models
{
    public static class LayoutModel
    {
        public static string APIUrl { get; set; }
        public static List<MenuViewDto> MenuItems { get; set; } = new List<MenuViewDto>();
        public static List<CurrencyDto> CurrencyItems { get; set; } = new List<CurrencyDto>();
        public static string WebUIUrl { get; set; }
        public static string WebMobileUIUrl { get; set; }
        public static List<CityDto> CityItems { get; set; } = new List<CityDto>();
        public static Option Option { get; set; }

        public static void LoadLayoutData(IConfiguration configuration, IMainPageRepository mainPageRepository, IOptionRepository optionRepository, ICityRepository cityRepository)
        {
            APIUrl = configuration.GetValue<string>("APIUrl");
            WebUIUrl = configuration.GetValue<string>("WebUIUrl");
            WebMobileUIUrl = configuration.GetValue<string>("WebMobileUIUrl");
            var model = mainPageRepository.GetMenuList().Result;
            if (model.DataResultIsNotNull())
            {
                MenuItems.AddRange(model.Data);
            }
            var option = optionRepository.GetOption().Result;
            if (option.DataResultIsNotNull())
            {
                Option = option.Data;
            }
            var cities = cityRepository.GetList().Result;
            if (cities.DataResultIsNotNull())
            {
                CityItems = cities.Data;
            }
        }
    }
}

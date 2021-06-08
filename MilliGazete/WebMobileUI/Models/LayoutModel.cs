using Entity.Dtos;
using Entity.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using WebMobileUI.Repository.Abstract;

namespace WebMobileUI.Models
{
    public static class LayoutModel
    {
        public static string APIUrl { get; set; }
        public static List<MenuDto> MenuItems { get; set; } = new List<MenuDto>();
        public static List<CurrencyDto> CurrencyItems { get; set; } = new List<CurrencyDto>();
        public static string WebUIUrl { get; set; }
        public static string WebMobileUIUrl { get; set; }
        public static Option Option { get; set; }

        public static void LoadLayoutData(IConfiguration configuration, IMainPageRepository mainPageRepository, IOptionRepository optionRepository)
        {
            APIUrl = configuration.GetValue<string>("APIUrl");
            WebUIUrl = configuration.GetValue<string>("WebUIUrl");
            WebMobileUIUrl = configuration.GetValue<string>("WebMobileUIUrl");
            var model = mainPageRepository.GetMenuList();
            if (model.DataResultIsNotNull())
            {
                MenuItems.AddRange(model.Data);
            }
            var option = optionRepository.GetOption();
            if (option.DataResultIsNotNull())
            {
                Option = option.Data;
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using WebUI.Models;
using WebUI.Repository.Abstract;

namespace WebUI.Services
{
    public class ActionFilter : IActionFilter
    {
        private readonly IMainPageRepository _mainPageRepository;
        private readonly IOptionRepository _optionRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IConfiguration configuration;

        public ActionFilter(IMainPageRepository mainPageRepository, IOptionRepository optionRepository, IConfiguration configuration, ICityRepository cityRepository)
        {
            _mainPageRepository = mainPageRepository;
            _optionRepository = optionRepository;
            _cityRepository = cityRepository;
            this.configuration = configuration;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public async void OnActionExecuting(ActionExecutingContext context)
        {
            if (configuration.GetSection("UseActionFilter").Get<bool>())
            {
                await LoadMenuItemsAsync(context);
                await LoadFinancialInfoAsync(context);
                await LoadOptionsAsync(context);
                await LoadCitiesAsync(context);
            }
        }

        private async Task LoadFinancialInfoAsync(ActionExecutingContext context)
        {
            var model = await _mainPageRepository.GetCurrencyList();
            if (model.Success) LayoutModel.CurrencyItems = model.Data;
        }
        private async Task LoadMenuItemsAsync(ActionExecutingContext context)
        {
            var model = await _mainPageRepository.GetMenuList();
            if (model.Success) LayoutModel.MenuItems = model.Data;
        }
        private async Task LoadCitiesAsync(ActionExecutingContext context)
        {
            var model = await _cityRepository.GetList();
            if (model.Success) LayoutModel.CityItems = model.Data;
        }
        private async Task LoadOptionsAsync(ActionExecutingContext context)
        {
            var model = await _optionRepository.GetOption();
            if (model.Success) LayoutModel.Option = model.Data;
        }
    }
}

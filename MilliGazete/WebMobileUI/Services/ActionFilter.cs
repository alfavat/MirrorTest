using Microsoft.AspNetCore.Mvc.Filters;
using WebMobileUI.Models;
using WebMobileUI.Repository.Abstract;

namespace WebMobileUI.Services
{
    public class ActionFilter : IActionFilter
    {
        private readonly IMainPageRepository _mainPageRepository;
        private readonly IOptionRepository _optionRepository;

        public ActionFilter(IMainPageRepository mainPageRepository, IOptionRepository optionRepository)
        {
            _mainPageRepository = mainPageRepository;
            _optionRepository = optionRepository;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            LoadMenuItems(context);
            LoadFinancialInfo(context);
            LoadOptions(context);
        }

        private void LoadFinancialInfo(ActionExecutingContext context)
        {
            var model = _mainPageRepository.GetCurrencyList();
            if (model.Success) LayoutModel.CurrencyItems = model.Data;
        }
        private void LoadMenuItems(ActionExecutingContext context)
        {
            var model = _mainPageRepository.GetMenuList();
            if (model.Success) LayoutModel.MenuItems = model.Data;
        }
        private void LoadOptions(ActionExecutingContext context)
        {
            var model = _optionRepository.GetOption();
            if (model.Success) LayoutModel.Option = model.Data;
        }
    }
}

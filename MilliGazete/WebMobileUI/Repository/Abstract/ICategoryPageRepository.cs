using Core.Utilities.Results;
using Entity.Dtos;
using System.Collections.Generic;
using WebMobileUI.Models;

namespace WebMobileUI.Repository.Abstract
{
    public interface ICategoryPageRepository : IUIBaseRepository
    {
        IDataResult<List<CustomHeading>> GetLastMainHeadingNewsByCategoryUrl(string categoryUrl, int limit = 0);
        IDataResult<List<MainPageCategoryNewsDto>> GetLastNewsByCategoryUrl(string categoryUrl, int limit = 0);
        IDataResult<CategoryDto> GetByUrl(string categoryUrl);
    }
}

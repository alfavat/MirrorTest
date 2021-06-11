using Entity.Abstract;
using Entity.Enums;
using System.Linq;

namespace Entity.Models
{
    public partial class News : IEntity
    {
        public string GetUrl()
        {
            if (NewsTypeEntityId == (int)NewsTypeEntities.Article)
            {
                return "/makale/" + Url;
            }
            return "/" + NewsCategories.FirstOrDefault()?.Category?.Url + "/" + Url + "-" + HistoryNo.ToString();
        }
    }
}

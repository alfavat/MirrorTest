using Core.Utilities.Results;
using Entity.Models;
using System.Threading.Tasks;
using WebUI.Repository.Abstract;

namespace WebUI.Repository.Concrete
{
    public class OptionRepository : IOptionRepository
    {
        public async Task<IDataResult<Option>> GetOption()
        {
            var result = await ApiHelper.GetApiAsync<Option>("Options/get");
            if (result.DataResultIsNotNull())
            {
                return result;
            }
            else
            {
                Option option = new Option()
                {
                    Address = "Adres",
                    Email = "mail@mail.com",
                    Facebook = "https://facebook.com",
                    Fax = "",
                    Instagram = "https://instagram.com",
                    LiveVideoLink = "https://www.youtube.com/embed/_CQXj3aD1LI",
                    PageRefreshPeriod = 10,
                    SeoDescription = "Doğru, güvenilir ve tarafsız habercilik",
                    SeoKeywords = "Seo keywords",
                    Telephone = "02120000000",
                    Twitter = "https://twitter.com",
                    WebsiteTitle = "Haberler, Son Dakika Haberleri ve Güncel Haberler",
                    Youtube = "https://youtube.com",
                    WebsiteSlogan = "Doğru, güvenilir ve tarafsız habercilik",
                    Id = 1
                };
                var result2 = new SuccessDataResult<Option>(data: option);
                return result2;
            }

        }
    }
}

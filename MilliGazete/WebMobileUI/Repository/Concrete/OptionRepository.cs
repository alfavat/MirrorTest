using Core.Utilities.Results;
using Entity.Models;
using WebMobileUI.Repository.Abstract;

namespace WebMobileUI.Repository.Concrete
{
    public class OptionRepository : IOptionRepository
    {
        public IDataResult<Option> GetOption()
        {
            var result = ApiHelper.GetApi<Option>("Options/get");
            if (result != null && result.Success && result.Data != null)
            {
                return result;
            }
            else
            {
                var option = new Option()
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

                return new SuccessDataResult<Option>(data: option);
            }

        }
    }
}

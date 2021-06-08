using ServerService.Abstract;
using ServerService.Helper;

namespace ServerService.Concrete
{
    public class SiteMapManager : ISiteMapService
    {
        public void CreateSiteMapXml()
        {
            SiteMapXmlHelper.CreateSiteMapXml();
            StaticXmlHelper.CreateStaticXml();
            PagesXmlHelper.CreatePagesXml();
            CategoryXmlHelper.CreateCategoriesXml();
        }
    }
}

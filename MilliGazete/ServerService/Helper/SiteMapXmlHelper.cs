using System.Xml;

namespace ServerService.Helper
{
    public static class SiteMapXmlHelper
    {
        public static void CreateSiteMapXml()
        {
            CreateXml(AppSettingsHelper.WebUIMapBasePath, AppSettingsHelper.MainUrl + "/");
            CreateXml(AppSettingsHelper.MobileUIBasePath, AppSettingsHelper.MobileMainUrl + "/");
        }

        private static void CreateXml(string basePath, string mainUrl)
        {
            string xNamespace = AppSettingsHelper.XmlNamespace;
            using (XmlWriter writer = XmlWriter.Create(basePath + @"sitemap.xml", new XmlWriterSettings
            {
                Encoding = System.Text.Encoding.UTF8
            }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("sitemapindex", xNamespace);
                writer.WriteAttributeString("xmlns", xNamespace);

                writer.WriteStartElement("sitemap");
                writer.WriteElementString("loc", mainUrl);
                writer.WriteElementString("changefreq", "hourly");
                writer.WriteEndElement();

                writer.WriteStartElement("sitemap");
                writer.WriteElementString("loc", mainUrl + "categories.xml");
                writer.WriteElementString("changefreq", "hourly");
                writer.WriteEndElement();

                writer.WriteStartElement("sitemap");
                writer.WriteElementString("loc", mainUrl + "pages.xml");
                writer.WriteElementString("changefreq", "hourly");
                writer.WriteEndElement();

                writer.WriteStartElement("sitemap");
                writer.WriteElementString("loc", mainUrl + "static.xml");
                writer.WriteElementString("changefreq", "hourly");
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
}

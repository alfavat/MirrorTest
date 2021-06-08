using Entity.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml;
using static ServerService.Helper.HttpHelper;
namespace ServerService.Helper
{
    public static class PagesXmlHelper
    {
        public static List<PageDto> GetPageList()
        {
            try
            {
                using (HttpHelper http = new HttpHelper())
                {
                    var saveRequest = http.Request<dynamic>(new RequestObject()
                    {
                        Url = AppSettingsHelper.ApiLink + "pages/getlist",
                        AuthorizationHeaderValue = "bearer " + AccessToken.Token,
                        Method = "GET"
                    });
                    var data = JsonConvert.DeserializeObject<List<PageDto>>(saveRequest.data.ToString());
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void CreatePagesXml()
        {
            var staticList = GetPageList();
            CreateXml(AppSettingsHelper.WebUIMapBasePath, AppSettingsHelper.MainUrl + "/", staticList);
            CreateXml(AppSettingsHelper.MobileUIBasePath, AppSettingsHelper.MobileMainUrl + "/", staticList);
        }

        private static void CreateXml(string basePath, string mainUrl, List<PageDto> staticList)
        {
            string xNamespace = AppSettingsHelper.XmlNamespace,
                            xsi = AppSettingsHelper.Xsi,
                         schemeLocation = AppSettingsHelper.SchemeLocation;
            using (XmlWriter writer = XmlWriter.Create(basePath + "pages.xml", new XmlWriterSettings
            {
                Encoding = System.Text.Encoding.UTF8
            }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("urlset", xNamespace);
                writer.WriteAttributeString("xmlns", xNamespace);
                writer.WriteAttributeString("xmlns", "xsi", null, xsi);
                writer.WriteAttributeString("xsi", "schemaLocation", null, schemeLocation);

                foreach (var item in staticList)
                {
                    writer.WriteStartElement("url");
                    writer.WriteElementString("loc", mainUrl + "sayfa/" +  item.Url);
                    writer.WriteElementString("changefreq", "weekly");
                    writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));
                    writer.WriteElementString("priority", "0.5");
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
}

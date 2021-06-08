using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Xml;

namespace ServerService.Helper
{
    public static class StaticXmlHelper
    {
        public static List<SiteMapItem> GetStaticList()
        {
            var staticList = new List<SiteMapItem>()
            {
                new SiteMapItem
                {
                    ChangeFreq = "weekly",
                    LastMod = DateTime.Now,
                    Loc = "yazarlar",
                    Priority = "0.5"
                },
                new SiteMapItem
                {
                    ChangeFreq = "weekly",
                    LastMod = DateTime.Now,
                    Loc = "canli-yayin",
                    Priority = "0.5"
                },
                new SiteMapItem
                {
                    ChangeFreq = "weekly",
                    LastMod = DateTime.Now,
                    Loc = "iletisim",
                    Priority = "0.5"
                },
                new SiteMapItem
                {
                    ChangeFreq = "weekly",
                    LastMod = DateTime.Now,
                    Loc = "foto-galeri",
                    Priority = "0.5"
                },
                 new SiteMapItem
                {
                    ChangeFreq = "weekly",
                    LastMod = DateTime.Now,
                    Loc = "video-galeri",
                    Priority = "0.5"
                }
            };
            return staticList;
        }

        public static void CreateStaticXml()
        {
            var list = GetStaticList();
            CreateXml(AppSettingsHelper.WebUIMapBasePath, AppSettingsHelper.MainUrl + "/", list);
            CreateXml(AppSettingsHelper.MobileUIBasePath, AppSettingsHelper.MobileMainUrl + "/", list);
        }
        private static void CreateXml(string basePath, string mainUrl, List<SiteMapItem> staticList)
        {
            string xNamespace = AppSettingsHelper.XmlNamespace,
                   xsi = AppSettingsHelper.Xsi,
               schemeLocation = AppSettingsHelper.SchemeLocation;
            using (XmlWriter writer = XmlWriter.Create(basePath + @"static.xml", new XmlWriterSettings
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
                    writer.WriteElementString("loc", mainUrl + item.Loc);
                    writer.WriteElementString("changefreq", item.ChangeFreq);
                    writer.WriteElementString("lastmod", item.LastMod.ToString("yyyy-MM-ddTHH:mm:ss"));
                    writer.WriteElementString("priority", item.Priority);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
}

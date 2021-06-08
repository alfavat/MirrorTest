using Entity.Dtos;
using System.Xml;

namespace ServerService.Helper
{
    public class AaNewsHelper
    {
        public static NewsAgencyAddDto GetNewsDetailsFromXml(string responseString)
        {
            var dto = new NewsAgencyAddDto();

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(responseString);
            var desc = xmlDoc.GetElementsByTagName("description");
            if (desc != null && desc.Count > 0)
            {
                dto.Description = desc.Item(0).InnerText;
            }
            var headline = xmlDoc.GetElementsByTagName("headline");
            if (headline != null && headline.Count > 0)
            {
                dto.Title = headline.Item(0).InnerText;
            }
            var content = xmlDoc.GetElementsByTagName("body.content");
            if (content != null && content.Count > 0)
            {
                dto.Description = xmlDoc.GetElementsByTagName("body.content").Item(0).InnerText;
            }

            var locations = xmlDoc.GetElementsByTagName("located");
            if (locations != null && locations.Count > 0)
            {
                var item = locations.Item(0);
                foreach (XmlNode name in item.ChildNodes)
                {
                    if (name.Attributes["xml:lang"] != null && name.Attributes["xml:lang"].Value.ToLower() == "tr")
                    {
                        dto.City = name.InnerText;
                        break;
                    }
                }
            }

            var categories = xmlDoc.GetElementsByTagName("channel");
            if (categories != null && categories.Count > 0)
            {
                var item = categories.Item(0);
                foreach (XmlNode name in item.ChildNodes)
                {
                    if (name.Attributes["xml:lang"] != null && name.Attributes["xml:lang"].Value.ToLower() == "tr")
                    {
                        dto.Category = name.InnerText;
                        break;
                    }
                }
            }
            return dto;

        }
    }
}
